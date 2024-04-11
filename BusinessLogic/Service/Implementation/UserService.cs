using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Model;
using BusinessLogic.Service.Interface;
using BusinessLogic.Utils;
using BusinessLogic.Validator;
using FluentValidation;
using Postgres.Entity;
using Postgres.Repository.Interface;

namespace BusinessLogic.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IValidator<UserRegisterModel> _userRegisterValidator;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IValidator<UserRegisterModel> userRegisterValidator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userRegisterValidator = userRegisterValidator;
            _mapper = mapper;
        }

        public async Task Register(UserRegisterModel user)
        {
            var validationResult = await _userRegisterValidator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entity = _mapper.Map<User>(user);
            entity.RegistrationDate = DateTime.UtcNow;
            var salt = Guid.NewGuid().ToString();
            entity.Salt = salt;
            entity.PasswordHash = PasswordHashService.GeneratePasswordHash(salt, user.Password);

            await _userRepository.InsertUser(entity);
        }

        public async Task<TokenPair> Login(UserRegisterModel user)
        {
            var validationResult = await _userRegisterValidator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var userEntity = await _userRepository.GetUserByLogin(user.Login);

            if (!PasswordHashService.Verify(userEntity.Salt, user.Password, userEntity.PasswordHash))
            {
                throw new AuthenticationException("Invalid password");
            }

            var tokenPair = JwtTokenGenerator.GenerateToken(userEntity);
            userEntity.AccessToken = tokenPair.AccessToken;
            userEntity.RefreshToken = tokenPair.RefreshToken;

            await _userRepository.UpdateUser(userEntity);

            return tokenPair;
        }
    }
}