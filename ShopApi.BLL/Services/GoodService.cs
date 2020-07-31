using System;
using System.Threading.Tasks;
using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Response;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.Core.Domain;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.BLL.Services
{
    public class GoodService : IGoodService
    {
        private readonly IGoodRepository goodRepository;
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;
        public GoodService(IGoodRepository goodRepository, IUnitOfwork unitOfwork, IMapper mapper)
        {
            this.goodRepository = goodRepository;
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }
        public async Task<GoodResponse> DeleteAsync(int id)
        {
            var existingGood = await goodRepository.FindByIDAsync(id);
            if(existingGood == null)
            {
                return new GoodResponse("Category not found");
            }
            try
            {
                goodRepository.Remove(existingGood);
                await unitOfwork.CompleteAsync();
                return new GoodResponse(existingGood);
            }
            catch (Exception ex)
            {
                return new GoodResponse($"Error when deleting good: {ex.Message}");
            }
        }

        public async Task<PagedList<GoodDTO>> ListAsync(GoodParams productParams)
        {
            var goods = await goodRepository.ListAsync(productParams);
            return mapper.Map<PagedList<Good>, PagedList<GoodDTO>>(goods);
        }

        public async Task<GoodResponse> SaveAsync(GoodDTO goodDTO)
        {
            Good good = mapper.Map<Good>(goodDTO);
            try
            {
                await goodRepository.AddASync(good);
                await unitOfwork.CompleteAsync();

                return new GoodResponse(good);
            }
            catch (Exception ex)
            {
                return new GoodResponse($"Error when seving the good: {ex.Message}");
            }
        }

        public async Task<GoodResponse> UpdateAsync(int id, GoodDTO goodDTO)
        {
            Good good = mapper.Map<Good>(goodDTO);
            var existingGood = await goodRepository.FindByIDAsync(id);
            if(existingGood == null)
            {
                return new GoodResponse("Good not found");
            }

            existingGood.GoodName = good.GoodName;
            existingGood.Category = good.Category;
            existingGood.Manufacturer = good.Manufacturer;
            existingGood.GoodCount = good.GoodCount;
            existingGood.Available = good.Available;
            existingGood.GoodPriceMinimal = good.GoodPriceMinimal;
            existingGood.GoodPriceActual = good.GoodPriceActual;
            existingGood.GoodImageURL = good.GoodImageURL;
            existingGood.CategoryId = good.CategoryId;
            existingGood.ManufacturerId = good.ManufacturerId;

            try
            {
                goodRepository.Update(existingGood);
                await unitOfwork.CompleteAsync();
                return new GoodResponse(existingGood);
            }
            catch (Exception ex)
            {
                return new GoodResponse($"Error when updating good: {ex.Message}");
            }
        }
    }
}