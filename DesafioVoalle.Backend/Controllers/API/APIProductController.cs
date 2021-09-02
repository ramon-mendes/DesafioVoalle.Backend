using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DesafioVoalle.Backend.Classes;
using DesafioVoalle.Backend.DAL;
using DesafioVoalle.Backend.Models;
using DesafioVoalle.Backend.Models.API;
using Microsoft.AspNetCore.Mvc;

namespace DesafioVoalle.Backend.Controllers.API
{
	[Route("api/product")]
	[ApiController]
	public class APIProductController : BaseAPIController
	{
		public APIProductController(MVCContext db)
		{
			_db = db;
		}

		// api/product/list
		[Route("list")]
		public List<Product> List()
		{
			var user = GetAuthorizedUser();
			var products = _db.Products.Where(p => p.UserId == user.Id);
			
			return products.Select(p => new Product()
			{
				Id = p.Id,
				Name = p.Name,
				Category = p.Category,
				Price = p.Price,
				ImagesURL = p.Images.Select(img => img.Url).ToList(),
			}).ToList();
		}

		// api/product/create
		[HttpPost]
		[Route("create")]
		public async Task<ActionResult> Create(Product model)
		{
			var user = GetAuthorizedUser();

			// upload images to Azure Blob Storage
			List<string> images = new List<string>();
			foreach(var item in model.ImagesURL)
				images.Add(await UploadMediaBase64(item));

			ProductModel prod;
			_db.Products.Add(prod = new ProductModel
			{
				UserId = user.Id,
				Name = model.Name,
				Price = model.Price,
				Category = model.Category,
			});
			_db.SaveChanges();

			// save images to db
			foreach(var item in images)
			{
				_db.ProductImages.Add(new ProductImageModel()
				{
					ProductId = prod.Id,
					Url = item,
				});
			}
			_db.SaveChanges();

			return Ok();
		}

		public async Task<string> UploadMediaBase64(string url)
		{
			if(url.StartsWith("http"))
				return url;// no need to upload
			if(url.StartsWith("data:"))
				url = url.Substring(url.IndexOf(',') + 1);
			var helper = new StorageHelper();
			return await helper.UploadAsync(new MemoryStream(Convert.FromBase64String(url)), Guid.NewGuid().ToString());
		}

		// api/product/edit
		[HttpPost]
		[Route("edit")]
		public async Task<ActionResult> Edit(Product model)
		{
			// upload images to Azure Blob Storage
			List<string> images = new List<string>();
			foreach(var item in model.ImagesURL)
				images.Add(await UploadMediaBase64(item));

			ProductModel prod = _db.Products.Find(model.Id);
			prod.Name = model.Name;
			prod.Category = model.Category;

			foreach(var item in prod.Images)
				_db.ProductImages.Remove(item);

			// save images to db
			foreach(var item in images)
			{
				_db.ProductImages.Add(new ProductImageModel()
				{
					ProductId = prod.Id,
					Url = item,
				});
			}
			_db.SaveChanges();

			return Ok();
		}

		// api/product/remove
		[Route("remove")]
		public ActionResult Remove(Guid id)
		{
			var user = GetAuthorizedUser();
			var product = _db.Products.Find(id);
			foreach(var item in product.Images)
				_db.ProductImages.Remove(item);
			_db.Products.Remove(product);
			_db.SaveChanges();

			return Ok();
		}
	}
}
