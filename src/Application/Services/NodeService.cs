using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task<IEnumerable<Node>> GetNodes(CancellationToken cancellationToken)
        {
            var result = await this.nodeRepository.ReadAll(cancellationToken);

            foreach (var node in result)
            {
                node.LatestReading = await this.readingRepository
                    .GetLatestByClientId(node.ClientId, cancellationToken);
                node.ReadingsAvailable = await this.readingRepository
                    .GetReadingCount(node.ClientId, cancellationToken);
                node.ReadingDefinitions = await this.readingRepository
                    .GetReadingDefinitionsByClientId(node.ClientId, cancellationToken);
            }
            return result;
        }

        public async Task<Node> GetNodeById(string clientId, CancellationToken cancellationToken)
        {
            var node = await this.nodeRepository.ReadById(clientId, cancellationToken);
            node.LatestReading = await this.readingRepository.GetLatestByClientId(clientId, cancellationToken);
            node.ReadingsAvailable = await this.readingRepository.GetReadingCount(clientId, cancellationToken);
            return node;
        }

        public async Task<IEnumerable<Reading>> GetReadingsById(string clientId, CancellationToken cancellationToken)
        {
            var node = await this.nodeRepository.ReadById(clientId, cancellationToken);
            if (node == null)
            {
                throw new NullReferenceException(
                    $"Node with id {clientId} could not be found");
            }
            return await this.readingRepository.GetAllByClientId(clientId, cancellationToken);
        }

        public async Task<IEnumerable<ReadingDefinition>> GetDefinitions(CancellationToken cancellationToken)
        {
            var definitions = await this.readingRepository.GetReadingDefinitions(cancellationToken);
            return definitions;
        }

        public async Task<IEnumerable<ReadingDefinition>> GetDefinitionsByClientId(string clientId, CancellationToken cancellationToken)
        {
            var definitions = await this.readingRepository.GetReadingDefinitionsByClientId(clientId, cancellationToken);
            return definitions;
        }

        public async Task<IEnumerable<ReadingDefinition>> UpdateDefinitions(
            string clientId,
            IEnumerable<ReadingDefinition> definitions,
            CancellationToken cancellationToken)
        {
            var definitionList = definitions.ToList();
            foreach (var definition in definitionList)
            {
                definition.ClientId = clientId;
            }
            var result = await this.readingRepository.UpdateReadingDefinitions(clientId, definitionList, cancellationToken);
            if (result == definitionList.Count)
            {
                return definitions;
            }
            return null;
        }


    }
}