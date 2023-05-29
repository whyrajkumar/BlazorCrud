using BlazorCrud.Shared;

namespace BlazorCrud.Client.Servives.SuperHeroService
{
    public interface ISuperHeroService
    {
        List<SuperHero> Heroes { get; set; }
        List<Comic> Comics { get; set; }

        Task GetComics();
        //Task DeleteComics();
        //Task UpdateComics();
    

        Task GetSuperHeroes();

        //Task DeleteSuperHero();
        //Task UpdateSuperHero();
        Task<SuperHero> GetSingleHeroes(int id);
        Task CreateHero(SuperHero hero);
        Task UpdateHero(SuperHero hero);
        Task DeleteHero(int id);


    }
}
