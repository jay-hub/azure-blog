using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ISVInternational.Core.ServiceRepos;
using ISVInternational.Entities.DTO;
using ISVInternational.Web.Helper;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ISVInternational.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(Guid? userId)
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
            var user = AuthProcessor.ProcessAuthToken(claims);
            var userDto = UserRepo.Instance.GetOrCreateUser(user);
            
            return View(userDto);
        }

        [HttpPost]
        public ActionResult UpdateProfile(UserDetailsDto dto)
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
            var user = AuthProcessor.ProcessAuthToken(claims);
            var userDto = UserRepo.Instance.GetOrCreateUser(user);
            dto.ProfileUrl = userDto.ProfileUrl;
            userDto.DisplayName = dto.DisplayName;
            var profileUrl = string.Empty;
            try
            {
                profileUrl = UploadBlob(dto, profileUrl);
            }
            catch (Exception e)
            {
                
            }
            userDto.ProfileUrl = profileUrl;
            UserRepo.Instance.UpdateProfile(userDto);

            return RedirectToAction("Index");
        }

        private static string UploadBlob(UserDetailsDto dto, string profileUrl)
        {
            if (dto.Files.Any(f => f != null))
            {
                Guid blobId = Guid.NewGuid();

                //Using the Azure Configuration Manager is optional. You can also use an API like the .NET Framework's ConfigurationManager class.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference("profileimages");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobId.ToString() + Path.GetExtension(dto.Files[0].FileName));
                blockBlob.Properties.ContentType = dto.Files[0].ContentType;

                // Create or overwrite the "myblob" blob with contents from a local file.
                blockBlob.UploadFromStream(dto.Files[0].InputStream);
                var a = blockBlob.Uri.AbsoluteUri;

                profileUrl = a;
            }

            return profileUrl;
        }
    }
    
}