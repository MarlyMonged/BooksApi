using AutoMapper;
using BooksApi.Dtos.Publisher;
using BooksApi.Interfaces;
using BooksApi.Models;

namespace BooksApi.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

      
        public async Task<IEnumerable<PublisherWithBooksAndAuthorsDto?>> GetAllPublishers()
        {
            var publishers = await _unitOfWork.Publishers.GetAllAsync();
            var publisherDtos = _mapper.Map<IEnumerable<PublisherWithBooksAndAuthorsDto>>(publishers);
            return publisherDtos;   
        }
        public async Task<PublisherWithBooksAndAuthorsDto?> GetPublisherWithBooksAndAuthors(int publisherId)
        {
            var publisher =await _unitOfWork.Publishers.GetPublisherWithBooksAndAuthors(publisherId);

            if (publisher == null)
                return null;

            var publishertDto = _mapper.Map<PublisherWithBooksAndAuthorsDto>(publisher);

            return publishertDto;

        }


        public async Task<int> CreatePublisherAsync(CreatePublisherDto dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Publisher name cannot be null or empty.");

             var publisher = _mapper.Map<Publisher>(dto);

            await _unitOfWork.Publishers.AddAsync(publisher);
            await _unitOfWork.Save();

            return publisher.Id;
        }


        public async Task<bool> UpdatePublisherAsync(int publisherId, CreatePublisherDto dto)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(publisherId);

            if (publisher is null) return false;

            _mapper.Map(dto, publisher);
            await _unitOfWork.Publishers.UpdateAsync(publisher);
            await _unitOfWork.Save();
            return true;
        }

        public async Task<bool> DeletePublisherAsync(int publisherId)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(publisherId);

            if (publisher is null) return false;

            await _unitOfWork.Publishers.DeleteAsync(publisherId);
            await _unitOfWork.Save();

            return true;

        }

    }
}
