using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;
using static Android.Content.ClipData;

namespace UrbanVogue.ViewModels
{
    public partial class ProductPageVM : BaseViewModel
    {
        private readonly Core _core;

        [ObservableProperty]
        private DetailedProduct _product;

        public ProductPageVM(Core core)
        {
            _core = core;
        }

        public override async Task InitAsync()
        {
            int id = 1;
            var product = await _core.GetProductDetailsAsync(id);

            Product = new DetailedProduct
            {
                Id = product.Id,
                Name = product.Name,
                BasePrice = product.BasePrice,
                DiscountPrice = product.DiscountPrice,
                Images = product.Images.ConvertAll(x => $"data:image/png;base64,{Convert.ToBase64String(x.Data)}"),
                Rating = product.Rating
            };
        }
    }
}
