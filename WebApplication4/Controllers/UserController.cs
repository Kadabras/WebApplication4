using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using WebApplication4.EfStuff;
using WebApplication4.EfStuff.DbModel;
using WebApplication4.EfStuff.Repositories;
using WebApplication4.Models;
using WebApplication4.Services;
using WebMaze.MyExceptions;

namespace WebApplication4.Controllers
{
    public class UserController : Controller
    {
        private IMapper _mapper;
        private UserRepository _userRepository;
        private RoleRepository _roleRepository;

        public UserController(IMapper mapper,
            UserRepository userRepository,
            RoleRepository roleRepository
            )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserList(int page = 1, int perPage = 10, string typeSorted = "Name",
                                      string filterString = "", string typeFilter = "Name", bool isDescending = false)
        {
            var users = _userRepository
                   .SortedBy(typeSorted, filterString, typeFilter, isDescending)
                   .ToList();

            var paggedUsersViewModel = users.Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList()
                .Select(dbModel => _mapper.Map<UserViewModel>(dbModel))
                .ToList();

            var indexViewModel = new UserListViewModel
            {
                PaggerViewModel = new PaggerViewModel<UserViewModel>
                {
                    PerPage = perPage,
                    CurrPage = page,
                    TotalRecordsCount = users.Count(),
                    Records = paggedUsersViewModel,
                },
                LastSort = typeSorted,
                LastFilter = typeFilter,
                LastString = filterString,
                AllTypes = typeof(UserViewModel).GetProperties().Select(x => x.Name).ToList(),
                IsDescending = isDescending

            };

            return View(indexViewModel);
        }

        public IActionResult RoleList()
        {
            var roles = _roleRepository.GetAll().Select(dbModel => _mapper.Map<RoleViewModel>(dbModel)).ToList();

            return View(roles);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserViewModel userViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            if (_userRepository.GetAll().Any(x => x.Email == userViewModel.Email))
            {
                ModelState.AddModelError("", "Email is already exists");
                return View(userViewModel);
            }

            var dbUser = _mapper.Map<User>(userViewModel);

            dbUser.Roles = new List<Role> { _roleRepository.GetAll().First(x => x.Name == Role.User) };
            _userRepository.Save(dbUser);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult ManageUser(long Id)
        {
            var userViewModel = _mapper.Map<UserViewModel>(_userRepository.Get(Id));

            var manageUserViewModel = new ManageUserViewModel
            {
                User = userViewModel,
                AllRoles = _roleRepository.GetAll().Select(x => x.Name).ToList()
            };

            return View(manageUserViewModel);
        }

        [HttpPost]
        public IActionResult ManageUser(ManageUserViewModel manageUserViewModel, string role)
        {
            var dbUser = _userRepository.Get(manageUserViewModel.User.Id);

            if (dbUser.Roles.Select(x => x.Name).FirstOrDefault() != role)
            {
                var addRole = _roleRepository.GetAll().FirstOrDefault(x => x.Name == role);
                dbUser.Roles.Add(addRole);
                _userRepository.Save(dbUser);
            }

            return RedirectToAction("Index", "User");
        }

        public IActionResult DeleteUser(long Id)
        {
            _userRepository.Remove(Id);
            return RedirectToAction("Index", "User");
        }

        public IActionResult GetUser(long Id)
        {
            var user = _userRepository.Get(Id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return View(_mapper.Map<UserViewModel>(user));
        }

        public IActionResult UserNotFoundError()
        {
            return View();
        }
    }
}
