using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using DataAccess;
using Serilog;

namespace Application.Repositries
{
    public class MiscRepository
    {
        private readonly ILogger logger;
        private readonly IMongoConnection connection;

        public MiscRepository(ILogger logger, IMongoConnection connection)
        {
            this.logger = logger;
            this.connection = connection;
        }
        public Task<GeneralDataModel> GetMiscData()
        {
            throw new NotImplementedException();
        }
    }
}