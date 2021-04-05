using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRUDRecipeEF.BL.DTOs;
using CRUDRecipeEF.DAL.Entities;
using CRUDRecipeEF.DAL.Repositories;

namespace CRUDRecipeEF.BL.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantRepo _restaurantRepo;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantService(IMapper mapper, IRestaurantRepo restaurantRepo, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _restaurantRepo = restaurantRepo;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Name of the restaurant</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<string> AddMenuToRestaurant(MenuAddDTO menuAddDTO)
        {
            var restaurant = await _restaurantRepo.GetRestaurantByNameAsync(menuAddDTO.RestaurantName);
            restaurant.Menus.Add(_mapper.Map<Menu>(menuAddDTO));
            await _unitOfWork.SaveAsync();
            return restaurant.Name;
        }

        /// <summary>
        /// </summary>
        /// <param name="restaurantDTO"></param>
        /// <returns>name of the restaurant</returns>
        /// ///
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> AddRestaurant(RestaurantDTO restaurantDTO)
        {
            if (await _restaurantRepo.RestaurantExists(restaurantDTO.Name))
            {
                throw new ArgumentException("Restaurant exists");
            }

            await _restaurantRepo.AddRestaurantAsync(_mapper.Map<Restaurant>(restaurantDTO));
            await _unitOfWork.SaveAsync();

            return restaurantDTO.Name;
        }

        /// <summary>
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="restaurantName"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task RemoveMenuFromRestaurant(string menuName, string restaurantName)
        {
            var restaurant = await _restaurantRepo.GetRestaurantByNameAsync(restaurantName);
            var menu = restaurant.Menus.FirstOrDefault(m => m.Name.ToLower() == menuName.ToLower().Trim());

            if (menu == null)
            {
                throw new KeyNotFoundException("Restaurant doesn't exist");
            }

            restaurant.Menus.Remove(menu);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeleteRestaurant(string name)
        {
            var restaurant = await _restaurantRepo.GetRestaurantByNameAsync(name) ?? throw new KeyNotFoundException("Restaurant doesn't exist");
            _restaurantRepo.DeleteRestaurant(restaurant);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<RestaurantDTO> GetRestaurantByName(string name)
        {
            var restaurant = await _restaurantRepo.GetRestaurantByNameAsync(name);
            return restaurant == null ? null : _mapper.Map<RestaurantDTO>(restaurant);
        }
    }
}