using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class PurchaseService : IPurchaseService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IProductDetailsService _productDetailsService;
        public readonly IPurchaseRepository _purchaseRepository;
        public readonly IBrandRepository _brandRepository;
        public readonly IFormatRepository _formatRepository;
        public readonly IProductRepository _productRepository;
        public readonly IProductPurchaseRepository _productPurchaseRepository;

        public PurchaseService(IUnitOfWork unitOfWork,
            IProductDetailsService productDetailsService,
            IPurchaseRepository purchaseRepository,
            IBrandRepository brandRepository,
            IFormatRepository formatRepository,
            IProductRepository productRepository,
            IProductPurchaseRepository productPurchaseRepository)
        {
            _unitOfWork = unitOfWork;
            _productDetailsService = productDetailsService;
            _purchaseRepository = purchaseRepository;
            _brandRepository = brandRepository;
            _formatRepository = formatRepository;
            _productRepository = productRepository;
            _productPurchaseRepository = productPurchaseRepository;
        }

        public async Task<PurchaseResponse> SavePurchaseAsync(Purchase purchase)
        {
            try
            {
                var newPurchase = await _purchaseRepository.AddAsync(purchase);
                await _unitOfWork.Commit();
                return new PurchaseResponse(newPurchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when saving a purchase: {ex.Message}");
            }
        }

        public async Task<PurchaseResponse> UpdatePurchaseAsync (Purchase purchase)
        {
            try
            {
                Purchase currentPurchase = await _purchaseRepository.GetByIdAsync(purchase.Id);

                currentPurchase.Date = purchase.Date;
                currentPurchase.StoreId = purchase.StoreId;
                await _unitOfWork.Commit();
                return new PurchaseResponse(purchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when updating a purchase: {ex.Message}");
            }
        }

        public async Task<PurchaseResponse> GetPurchaseByIdAsync (int id)
        {
            try
            {
                var purchase = await _purchaseRepository.GetAllDataByIdAsync(id);

                return new PurchaseResponse(purchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when getting a purchase with id {id}: {ex.Message}");
            }
        }

        public async Task<PurchaseResponse> FindPurchaseByIdAsync(int id)
        {
            try
            {
                var purchase = await _purchaseRepository.GetByIdAsync(id);

                return new PurchaseResponse(purchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when finding a purchase with id {id}: {ex.Message}");
            }
        }

        public async Task<ProductPurchaseResponse> AddProductToPurchase(int id, ProductPurchase product)
        {
            try
            {
                Purchase currentPurchase = await _purchaseRepository.GetWithProductsByIdAsync(id);

                if (currentPurchase == null)
                {
                    throw new Exception("La compra no existe en base de datos");
                }

                if (product.Weight > 0)
                {
                    product.ProductDetail = null;
                    product.ProductDetailId = null;
                }

                ProductPurchase currentProduct = currentPurchase.ProductList.Where(p => p.Id == product.Id).FirstOrDefault();

                if (currentProduct != null)
                {
                    //Estamos editando un producto ya existente

                    if (product.ProductDetailId != null)
                    {
                        //Comprobar los productDetails
                        var productDetail = await _productDetailsService.GetProductDetailsByDataAsync(product.ProductDetail.ProductId,
                            product.ProductDetail.BrandId.Value, product.ProductDetail.FormatId.Value);

                        if (productDetail != null)
                        {
                            productDetail.LastPrice = product.Price;
                            currentProduct.ProductDetailId = productDetail.Id;
                            currentProduct.ProductDetail = productDetail;
                        }
                        else
                        {
                            currentProduct.ProductDetail = new ProductDetails();
                            currentProduct.ProductDetail.BrandId = product.ProductDetail.BrandId.Value;
                            currentProduct.ProductDetail.FormatId = product.ProductDetail.FormatId.Value;
                            currentProduct.ProductDetail.ProductId = product.Id;
                            currentProduct.ProductDetail.LastPrice = product.Price;
                        }
                    }

                    currentProduct.Quantity = product.Quantity;
                    currentProduct.Weight = product.Weight;
                    currentProduct.Price = product.Price;

                    //Actualizamos el total de la compra
                    //currentPurchase.Total = currentPurchase.Total - currentProduct.Price + product.Price;
                    currentPurchase.Total = currentPurchase.ProductList.Sum(p => p.Price);
                    _purchaseRepository.Update(currentPurchase);
                }
                else
                {
                    if (product.ProductDetail != null)
                    {
                        //Comprobar los productDetails
                        var productDetail = await _productDetailsService.GetProductDetailsByDataAsync(product.ProductDetail.ProductId,
                            product.ProductDetail.BrandId, product.ProductDetail.FormatId);

                        if (productDetail != null)
                        {
                            productDetail.LastPrice = product.Price;
                            product.ProductDetailId = productDetail.Id;
                            product.ProductDetail = productDetail;
                        }
                        else
                        {
                            product.ProductDetail.LastPrice = product.Price;
                        }
                    }

                    currentPurchase.ProductList.Add(product);
                    currentPurchase.Total += product.Price;
                }
                // context.Entry(existingBlog).CurrentValues.SetValues(blog);
                await _unitOfWork.Commit();

                product = await _productPurchaseRepository.GetByIdAsync(product.Id);
                return new ProductPurchaseResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductPurchaseResponse($"An error occurred when adding a product to a purchase with id {id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _purchaseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ProductPurchase>> GetProductsPurchase(int id)
        {
            Purchase purchase = await _purchaseRepository.GetAllDataByIdAsync(id);

            return purchase.ProductList;
        }

        public async Task<ProductPurchaseResponse> DeleteProductFromPurchase(int idPurchase, int idProductPurchase)
        {
            try
            {
                Purchase purchase = await _purchaseRepository.GetWithProductsByIdAsync(idPurchase);
                ProductPurchase product = purchase.ProductList.SingleOrDefault(p => p.Id == idProductPurchase);

                if (product == null)
                {
                    throw new Exception($"No existe el producto {idProductPurchase} en la compra {idPurchase}");
                }

                purchase.ProductList.Remove(purchase.ProductList.Single(p => p.Id == idProductPurchase));
                purchase.Total -= product.Price;

                _purchaseRepository.Update(purchase);
                await _unitOfWork.Commit();

                return new ProductPurchaseResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductPurchaseResponse($"An error occurred when deleting a product with id {idProductPurchase} " +
                    $"from a purchase with id { idPurchase }: { ex.Message}");
            }
        }

        public async Task<PurchaseResponse> DeletePurchase(int idPurchase)
        {
            try
            {
                await _purchaseRepository.DeleteByIdAsync(idPurchase);
                await _unitOfWork.Commit();
                return new PurchaseResponse(new Purchase());
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error occurred when deleting the purchase with id {idPurchase}: {ex.Message}");
            }
        }
    }
}
