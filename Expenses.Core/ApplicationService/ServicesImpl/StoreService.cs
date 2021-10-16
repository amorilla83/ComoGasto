using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;
using Expenses.Core.Entities.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepo;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMemoryCache _cache;


        public StoreService (IStoreRepository storeRepostory, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _storeRepo = storeRepostory;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<StoreResponse> DeleteStoreAsync(int id)
        {
            var existingStore = await _storeRepo.GetByIdAsync(id);

            if (existingStore == null)
            {
                return new StoreResponse("Store not found");
            }

            try
            {
                await _storeRepo.DeleteByIdAsync(id);
                await _unitOfWork.Commit();
                UpdateCache();
                return new StoreResponse(existingStore);

            }
            catch (Exception ex)
            {
                return new StoreResponse($"An error occurred when removing the store: {ex.Message}");
            }
        }

        public async Task<StoreResponse> FindStoreByIdAsync(int id)
        {
            try
            {
                var store = await _storeRepo.GetByIdAsync(id);

                return new StoreResponse(store);
            }
            catch (Exception ex)
            {
                return new StoreResponse($"An error occurred when finding a store with id {id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            var stores = await _cache.GetOrCreateAsync(CacheKeys.StoresList, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _storeRepo.GetAllAsync();
            });

            return stores;
        }

        //public List<Store> GetAllStoresByName (string name)
        //{
        //    return _storeRepo.GetAll().Where(a => a.Name == name).ToList();
        //}

        public Store NewStore(string name, string logo)
        {
            Store store = new()
            {
                Name = name,
                Logo = logo
            };
            return store;
        }

        public async Task<StoreResponse> SaveStoreAsync(Store store)
        {
            try
            {
                var newStore = await _storeRepo.AddAsync(store);
                await _unitOfWork.Commit();
                UpdateCache();
                return new StoreResponse(newStore);
            }
            catch (Exception ex)
            {
                return new StoreResponse($"An error occurred when saving a store: {ex.Message}");
            }
        }

        public async Task<StoreResponse> UpdateStoreAsync(int id, Store storeUpdate)
        {
            var existingStore = await _storeRepo.GetByIdAsync(id);

            if (existingStore == null)
            {
                return new StoreResponse("Store not found");
            }

            existingStore.Name = storeUpdate.Name;
            existingStore.Logo = storeUpdate.Logo;

            try
            {
                _storeRepo.Update(existingStore);
                await _unitOfWork.Commit();
                UpdateCache();
                return new StoreResponse(existingStore);
            }
            catch (Exception ex)
            {
                return new StoreResponse($"An error occurred when updating the store: {ex.Message}");
            }
        }

        private void UpdateCache ()
        {
            try
            {
                _cache.Remove(CacheKeys.StoresList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
