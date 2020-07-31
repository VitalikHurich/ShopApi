using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Response;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.BLL.Services
{
    public class ManufacturerService: IService<ManufacturerDTO, ManufacturerResponse>
    {
        private readonly IRepository<Manufacturer> manufacturerRepository;
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;
        public ManufacturerService(IRepository<Manufacturer> manufacturerRepository, IUnitOfwork unitOfwork, IMapper mapper)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }
        public async Task<ManufacturerResponse> DeleteAsync(int id)
        {
            var existingManufacturer = await manufacturerRepository.FindByIDAsync(id);
            if(existingManufacturer == null)
            {
                return new ManufacturerResponse("Manufacturer not found");
            }
            try
            {
                manufacturerRepository.Remove(existingManufacturer);
                await unitOfwork.CompleteAsync();
                return new ManufacturerResponse(existingManufacturer);
            }
            catch (Exception ex)
            {
                return new ManufacturerResponse($"Error when deleting manufacturer: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ManufacturerDTO>> ListAsync()
        {
            var manufacturers = await manufacturerRepository.ListAsync();
            return mapper.Map<IEnumerable<ManufacturerDTO>>(manufacturers);
        }

        public async Task<ManufacturerResponse> SaveAsync(ManufacturerDTO manufacturerDTO)
        {
            Manufacturer manufacturer = mapper.Map<Manufacturer>(manufacturerDTO);
            try
            {
                await manufacturerRepository.AddASync(manufacturer);
                await unitOfwork.CompleteAsync();

                return new ManufacturerResponse(manufacturer);
            }
            catch (Exception ex)
            {
                return new ManufacturerResponse($"Error when seving the manufacturer: {ex.Message}");
            }
        }

        public async Task<ManufacturerResponse> UpdateAsync(int id, ManufacturerDTO manufacturerDTO)
        {
            Manufacturer manufacturer = mapper.Map<Manufacturer>(manufacturerDTO);
            var existingManufacturer = await manufacturerRepository.FindByIDAsync(id);
            if(existingManufacturer == null)
            {
                return new ManufacturerResponse("Manufacturer not found");
            }

            existingManufacturer.ManufacturerName = manufacturer.ManufacturerName;

            try
            {
                manufacturerRepository.Update(existingManufacturer);
                await unitOfwork.CompleteAsync();
                return new ManufacturerResponse(existingManufacturer);
            }
            catch (Exception ex)
            {
                return new ManufacturerResponse($"Error when updating manufacturer: {ex.Message}");
            }
        }
    }
}