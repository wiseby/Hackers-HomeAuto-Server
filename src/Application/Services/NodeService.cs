using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using Application.Repositries;

namespace Application.Services
{
    public class NodeService : INodeService
    {
        private readonly INodeRepository nodeRepository;
        private readonly IReadingRepository readingRepository;

        public NodeService(
            INodeRepository nodeRepository,
            IReadingRepository readingRepository)
        {
            this.nodeRepository = nodeRepository;
            this.readingRepository = readingRepository;
        }

        public async Task<IEnumerable<Node>> GetNodes()
        {
            var result = await this.nodeRepository.ReadAll();

            foreach (var node in result)
            {
                node.LatestReading = await this.readingRepository
                    .GetLatestByClientId(node.ClientId);
                node.ReadingsAvailable = await this.readingRepository
                    .GetReadingCount(node.ClientId);
            }
            return result;
        }

        public async Task<Node> GetNodeById(string clientId)
        {
            var node = await this.nodeRepository.ReadById(clientId);
            node.LatestReading = await this.readingRepository.GetLatestByClientId(clientId);
            node.ReadingsAvailable = await this.readingRepository.GetReadingCount(clientId);
            return node;
        }

        public async Task<IEnumerable<Reading>> GetReadingsById(string clientId)
        {
            var node = await this.nodeRepository.ReadById(clientId);
            if (node == null)
            {
                throw new NullReferenceException(
                    $"Node with id {clientId} could not be found");
            }
            return await this.readingRepository.GetAllByClientId(clientId);
        }

        public Task<IEnumerable<ReadingDefinition>> GetDefinitions()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Reading>> GetDefinitionsByClientId(string clientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ReadingDefinition>> UpdateDefinitions(
            string clientId, ReadingDefinition definition)
        {
            throw new System.NotImplementedException();
        }
    }
}