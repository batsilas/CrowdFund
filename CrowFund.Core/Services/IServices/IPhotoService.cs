using CrowFund.Core.Models;
using CrowFund.Core.Services.Options;
using CrowFund.Core.Services.Options.PhotoOptions;
using System.Linq;

namespace CrowFund.Core.Services.IServices
{
    public interface IPhotoService
    {
        Photo CreatePhoto(
            CreatePhotoOptions options);
        Photo UpdatePhoto(
            UpdatePhotoOptions options);

    }
}
