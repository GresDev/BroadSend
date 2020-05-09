using System.Collections.Generic;

namespace BroadSend.Server.Models.Contracts
{
    public interface ITitleRepository
    {
        IEnumerable<Title> AllTitles { get; }

        bool TitleNameIsUnique(string fullName);
        bool TitleAliasIsUnique(string alias);


        void CreateTitle(Title title, string titleAlias);

        Title GetTitle(int titleId);

        void UpdateTitle(Title title);

        void DeleteTitle(int titleId);



        void CreateAlias(TitleAlias titleAlias);

        TitleAlias GetAlias(int titleAiliasId);

        List<TitleAlias> GetAliases(int titleId);

        void UpdateAlias(TitleAlias titleAlias);

        void DeleteAlias(int aliasId);
    }
}
