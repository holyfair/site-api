using Models.Consts;
using Models.Enums;
using Models.Exceptions;
using Models.Shared;
using Models.Sources;
using Repository.DatabaseModels;
using Repository.UnitOfWork;
using Services.Interfaceses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class SourceService : ISourceService
    {
        private IUnitOfWork _unitOfWork;

        private readonly Dictionary<SourceTypes, Func<IBaseSource, string, Task<string>>> sourceFormaters;
        private readonly Dictionary<PublicationNumberTypes, string> shortPublicationTypes;

        public SourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            sourceFormaters = new Dictionary<SourceTypes, Func<IBaseSource, string, Task<string>>>
            {
                { SourceTypes.Electronic,  async (x, email) => await CreateElectronicSourceAsync(x, email) },
                { SourceTypes.Book, async (x, email) => await CreateBookSourceAsync(x, email) },
                { SourceTypes.Periodical, async (x, email) => await CreatePeriodicalSourceAsync(x, email) },
                { SourceTypes.Dissertation, async (x, email) => await CreateDissertationSourceAsync(x as DissertationSource, email) },
                { SourceTypes.AbstractOfDissertation, async (x, email) => await CreateDissertationSourceAsync(x as AbstractDissertationSource, email) }
            };

            shortPublicationTypes = new Dictionary<PublicationNumberTypes, string>
            {
                {PublicationNumberTypes.Book, "кн." },
                {PublicationNumberTypes.Edition, "вип." },
                {PublicationNumberTypes.Number, "вип." },
                {PublicationNumberTypes.Volume, "№" }
            };
        }

        public async Task<string> CreateSourceAsync(IBaseSource source, string userEmail = null)
        {
            return await sourceFormaters[source.Type](source, userEmail);
        }

        private async Task<string> CreateElectronicSourceAsync(IBaseSource source, string userEmail = null)
        {
            var electronicSource = source as ElectronicSource;
            var publication = string.IsNullOrEmpty(electronicSource.Publication) ? string.Empty : $" // {electronicSource.Publication}";
            var yearOfPulication = electronicSource.YearOfPublication == null ? string.Empty : $". – {electronicSource.YearOfPublication}.";

            var content = $"{electronicSource.ParseAuthor()}{electronicSource.WorkName} [Електронний ресурс]{electronicSource.ParseAllAuthors()}{publication}{yearOfPulication}" +
                $" – Режим доступу до ресурсу: {electronicSource.LinkToSource}";

            var newSource = new SourceRecord { Content = content, Type = electronicSource.Type };

            if(!string.IsNullOrEmpty(userEmail))
            {
                await SaveSourceAsync(newSource, userEmail);
            }

            return content;
        }

        private async Task<string> CreateBookSourceAsync(IBaseSource source, string userEmail = null)
        {
            var bookSource = source as BookSource;

            var workName = string.IsNullOrEmpty(bookSource.WorkName) ? string.Empty : $" {bookSource.WorkName} /";
            var placeOfPublication = string.IsNullOrEmpty(bookSource.PlaceOfPublication) ? string.Empty : $" - {bookSource.PlaceOfPublication}";
            var publishingHouse = string.IsNullOrEmpty(bookSource.PublishingHouse) ? string.Empty : $": {bookSource.PublishingHouse}";
            var yearOfPublication = bookSource.YearOfPublication == null ? string.Empty : $", {bookSource.YearOfPublication}.";
            var numberOfPages = string.IsNullOrEmpty(bookSource.NumberOfPages) ? string.Empty : $" – {bookSource.NumberOfPages} c.";
            var publishingName = string.IsNullOrEmpty(bookSource.PublishingName) ? string.Empty : $" – ({bookSource.PublishingName}).";

            var content = $"{bookSource.ParseAuthor()}{workName}" + 
                $"{bookSource.ParseAllAuthors()}{placeOfPublication}" +
                $"{publishingHouse}{yearOfPublication}" +
                $"{numberOfPages}{publishingName} – " +
                $"({bookSource.Series}; {shortPublicationTypes[bookSource.PublicationNumberType]} {bookSource.PeriodicSelectionNumber})";

            var newSource = new SourceRecord { Content = content, Type = bookSource.Type };

            if (!string.IsNullOrEmpty(userEmail))
            {
                await SaveSourceAsync(newSource, userEmail);
            }

            return content;
        }

        private async Task<string> CreatePeriodicalSourceAsync(IBaseSource source, string userEmail = null)
        {
            var periodicalSource = source as PeriodicalSource;

            var publication = string.IsNullOrEmpty(periodicalSource.Publication) ? string.Empty : $" // {periodicalSource.Publication}";
            var yearOfPulication = periodicalSource.YearOfPublication == null ? string.Empty : $". – {periodicalSource.YearOfPublication}.";
            var periodicSelectionNumber = periodicalSource.PeriodicSelectionNumber == null ? string.Empty : $" – №{periodicalSource.PeriodicSelectionNumber}.";
            var pages = string.IsNullOrEmpty(periodicalSource.Pages) ? string.Empty : $" – C. {periodicalSource.Pages}.";

            var content = $"{periodicalSource.ParseAuthor()}{periodicalSource.WorkName}{periodicalSource.ParseAllAuthors()}{publication}{yearOfPulication}" +
                $"{periodicSelectionNumber}{pages}";

            var newSource = new SourceRecord { Content = content, Type = periodicalSource.Type };
            if (!string.IsNullOrEmpty(userEmail))
            {
                await SaveSourceAsync(newSource, userEmail);
            }

            return content;
        }

        private async Task<string> CreateDissertationSourceAsync(DissertationSource source, string userEmail = null)
        {
            var dissertationSource = source as DissertationSource;

            ValidateDissertationSource(dissertationSource);

            var placeOfPublication = string.IsNullOrEmpty(dissertationSource.PlaceOfPublication) ? string.Empty : $" - {dissertationSource.PlaceOfPublication}";
            var yearOfPublication = dissertationSource.YearOfPublication == null ? string.Empty : $", {dissertationSource.YearOfPublication}";
            var numberOfPages = dissertationSource.NumberOfPages == null ? string.Empty : $". - {dissertationSource.NumberOfPages} с";

            var content = $"{dissertationSource.ParseAuthor()}{dissertationSource.WorkName}{dissertationSource.GetScientificDegree()}{dissertationSource.GetSpecialty()}" +
                $"{dissertationSource.ParseAllAuthors()}{placeOfPublication}{yearOfPublication}{numberOfPages}.";

            var newSource = new SourceRecord { Content = content, Type = dissertationSource.Type };
            if (!string.IsNullOrEmpty(userEmail))
            {
                await SaveSourceAsync(newSource, userEmail);
            }

            return content;
        }

        private void ValidateDissertationSource(DissertationSource source, string userEmail = null)
        {
            if(!string.IsNullOrEmpty(source.ScientificDegreeName) && !ScientificDegrees.ScientificDegreeNames.Keys.Contains(source.ScientificDegreeName))
            {
                throw new BadRequestException("Invalid scientific degree name!");
            }
            if (!string.IsNullOrEmpty(source.ScientificDegreeSpecialty) && !ScientificDegrees.ScientificDegreeSpecialties.Keys.Contains(source.ScientificDegreeSpecialty))
            {
                throw new BadRequestException("Invalid scientific degree specialty!");
            }
        }

        private async Task SaveSourceAsync(SourceRecord sourceRecord, string userEmail)
        {
            var user = await _unitOfWork.Users.GetUserAsync(userEmail);

            if(user is null)
            {
                throw new NotFoundException($"Cant find user by email: {userEmail}");
            }

            sourceRecord.User = user;
            await _unitOfWork.Sources.CreateSourceAsync(sourceRecord);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PagedResult<SourceRecord>> GetSourceAsync(GetListQuery query, string userEmail)
        {
            return await _unitOfWork.Sources.GetSourcesAsync(query, userEmail);
        }
    }
}
