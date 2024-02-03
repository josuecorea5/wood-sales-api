using AutoMapper;
using WoodSalesApi.DTOs;
using WoodSalesApi.Models;
using WoodSalesApi.Repositories;

namespace WoodSalesApi.Services
{
	public class ClientService : ICommonService<ClientDto, ClientInsertDto, ClientUpdateDto>
	{
		private readonly IRepository<Client> _clientRepository;
		private readonly IMapper _mapper;
		public List<string> Errors { get; }

		public ClientService(IRepository<Client> clientRepository, IMapper mapper)
		{
			_clientRepository = clientRepository;
			_mapper = mapper;
			Errors = new List<string>();
		}

		public async Task<IEnumerable<ClientDto>> GetAll()
		{
			var clients = await _clientRepository.GetAll();

			return clients.Select(c => _mapper.Map<ClientDto>(c));
		}

		public async Task<ClientDto> GetById<T>(T id)
		{
			var client = await _clientRepository.GetById<T>(id);

			if (client is null)
			{
				Errors.Add($"Client with id {id} not found");
				return null;
			};

			return _mapper.Map<ClientDto>(client);
		}

		public async Task<ClientDto> Add(ClientInsertDto entity)
		{
			var newClient = _mapper.Map<Client>(entity);
			await _clientRepository.Add(newClient);
			await _clientRepository.Save();

			return _mapper.Map<ClientDto>(newClient);
		}
		public async Task<ClientDto> Update<T>(T id, ClientUpdateDto entity)
		{
			var client = await _clientRepository.GetById<T>(id);

			if (client is null)
			{
				Errors.Add($"Client with id {id} not found");
				return null;
			}
			client.Name = entity.Name;
			_clientRepository.Update(client);
			await _clientRepository.Save();

			return _mapper.Map<ClientDto>(client);
		}

		public async Task<ClientDto> Delete<T>(T id)
		{
			var client = await _clientRepository.GetById<T>(id);

			if(client is null)
			{
				Errors.Add($"Client with id {id} not found");
				return null;
			}

			var clientDto = _mapper.Map<ClientDto>(client);
			_clientRepository.Delete(client);
			await _clientRepository.Save();

			return clientDto;
		}
	}
}
