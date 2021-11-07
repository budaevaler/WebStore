using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDbInitializer> _logger;

        public WebStoreDbInitializer(WebStoreDB db, ILogger<WebStoreDbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            _logger.LogInformation("Запуск инициализации БД");
            //var db_deleted= await _db.Database.EnsureDeletedAsync();
            var dbCreated=await _db.Database.EnsureCreatedAsync();
            var pendingMigrations = await _db.Database.GetPendingMigrationsAsync();
            var appliedMigrations= await _db.Database.GetAppliedMigrationsAsync();
            if (pendingMigrations.Any())
            {
                _logger.LogInformation($"Применение миграций: {string.Join(",", pendingMigrations)}");
                await _db.Database.MigrateAsync();
            }
            await InitializeProductAsync();
        }

        private async Task InitializeProductAsync()
        {
            if (_db.Sections.Any())
            {
                _logger.LogInformation("Инициализация БД не нужна");
                return;
            }
            _logger.LogInformation("Запись секций..");
            using (_db.Database.BeginTransactionAsync())
            {
                _db.Sections.AddRange(TestData.Sections);
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                await _db.Database.CommitTransactionAsync();
            }
            _logger.LogInformation("Запись секций выполнена успешно");

            _logger.LogInformation("Запись брендов..");
            using (_db.Database.BeginTransactionAsync())
            {
                _db.Brands.AddRange(TestData.Brands);
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                await _db.Database.CommitTransactionAsync();
            }
            _logger.LogInformation("Запись брендов выполнена успешно");

            _logger.LogInformation("Запись товаров...");
            using (_db.Database.BeginTransactionAsync())
            {
                _db.Products.AddRange(TestData.Products);
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");
                await _db.Database.CommitTransactionAsync();
            }
            _logger.LogInformation("Запись товаров выполнена успешно");

        }
    }


}
