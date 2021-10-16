using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class FormatService : IFormatService
    {
        private IUnitOfWork _unitOfWork;

        private readonly IFormatRepository _formatRepository;
        private readonly IBrandService _brandService;

        public FormatService (IUnitOfWork unitOfWork, IFormatRepository formatRepository,
            IBrandService brandService)
        {
            _unitOfWork = unitOfWork;
            _formatRepository = formatRepository;
            _brandService = brandService;
        }


        public async Task<FormatResponse> SaveFormatAsync(Format addFormat)
        {
            try
            {
                Format newFormat = new Format();
                //Exists format and associate with brand
                newFormat = await FindFormatByNameAsync(addFormat.Name);

                if (newFormat == null)
                {
                    newFormat = new Format()
                    {
                        Name = addFormat.Name
                    };
                }


                if (addFormat.BrandList.Any())
                {
                    if (newFormat.BrandList.Any(b => b.Id == addFormat.BrandList.First().Id))
                    {
                        throw new Exception($"El formato {addFormat.Name} ya está asociado con " +
                            $"la marca con Id {addFormat.BrandList.First().Id}");
                    }

                    Brand newBrand = await _brandService.FindBrandByIdAsync(addFormat.BrandList.First().Id);

                    if (newBrand == null)
                    {
                        throw new Exception($"La marca con Id {addFormat.BrandList.First().Id} no existe en base de datos");
                    }

                    newFormat.BrandList.Add(newBrand);
                }

                _formatRepository.Update(newFormat);

                await _unitOfWork.Commit();
                return new FormatResponse(newFormat);
            }
            catch (Exception ex)
            {
                return new FormatResponse($"An error occurred when saving a brand: {ex.Message}");
            }
        }

        private async Task<Format> FindFormatByNameAsync(string name)
        {
            Expression<Func<Format, bool>> predicate = (Format entity) => entity.Name == name;
            return await _formatRepository.GetByConditionAsync(predicate);
        }
    }
}
