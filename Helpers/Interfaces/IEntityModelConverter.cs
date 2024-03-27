using Microsoft.EntityFrameworkCore;
using System.Numerics;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Models;

namespace xilopro2.Helpers.Interfaces
{
    public interface IEntityModelConverter
    {
        Team ToTeamEntity(TeamViewModel model, bool isNew);

        TeamViewModel ToTeamViewModel(Team teamEntity);

        TorneoViewModel ToTournamentViewModel(Torneo entity);

        Torneo ToTournamentEntity(TorneoViewModel model, bool isNew);

        Task<Groups> ToGroupEntityAsync(GroupViewModel model, bool isNew);

        GroupViewModel ToGroupViewModel(Groups groupEntity);


    }

    public class EntityModelConverter : IEntityModelConverter
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly ICombos _combos;

        public EntityModelConverter(DataContext context, IImageHelper imageHelper,ICombos combos)
        {
            _context = context;
            _imageHelper = imageHelper;
            _combos = combos;

        }

        public Team ToTeamEntity(TeamViewModel model, bool isNew)
        {
            return new Team
            {
                Team_ID = isNew ? 0 : model.Team_ID,
                Team_Name = model.Team_Name?.Trim().ToUpper(),
                Team_Estadio = model.Team_Estadio == null ? string.Empty : model.Team_Estadio.ToUpper(),
                Team_Image = model.Team_Image,
            };

        }

        public TeamViewModel ToTeamViewModel(Team teamEntity)
        {
            return new TeamViewModel
            {
                Team_ID = teamEntity.Team_ID,
                Team_Name = teamEntity.Team_Name.ToUpper(),
                Team_Estadio = teamEntity.Team_Estadio.ToUpper(),
                Team_Image = teamEntity.Team_Image,
            };

        }

        //torneo model

        public TorneoViewModel ToTournamentViewModel(Torneo entity)
        {
            return new TorneoViewModel
            {
                Torneo_EndDate = entity.Torneo_EndDate,
                Groups = entity.Groups,
                Torneo_ID = entity.Torneo_ID,
                Torneo_Status = entity.Torneo_Status,
                Torneo_Image = entity.Torneo_Image,
                Torneo_Name = entity.Torneo_Name,
                Torneo_StartDate = entity.Torneo_StartDate,
                Torneo_Season = entity.Torneo_Season,
                SelectedCategoryIds = entity.SelectedCategoryIds,
              //  Categories = _combos.GetCategorias(),
            };
        }

        public Torneo ToTournamentEntity(TorneoViewModel model,  bool isNew)
        {
            return new Torneo
            {
                Torneo_EndDate = model.Torneo_EndDate.ToUniversalTime(),
                Groups = model.Groups,
                Torneo_ID = isNew ? 0 : model.Torneo_ID,
                Torneo_Status = model.Torneo_Status,
                Torneo_Image = isNew ? _imageHelper.UploadImage(model.LogoFile, "Tournaments") : model.Torneo_Image,
                Torneo_Name = model.Torneo_Name,
                Torneo_StartDate = model.Torneo_StartDate.ToUniversalTime(),
                Torneo_Season = model.Torneo_Season,
                SelectedCategoryIds = model.SelectedCategoryIds,
            };
        }

        public async Task<Groups> ToGroupEntityAsync(GroupViewModel model, bool isNew)
        {
            return new Groups
            {
               // GroupDetails = model.GroupDetails,
              //  Group_ID = isNew ? 0 : model.Group_ID,
               // Matches = model.Matches,
                Group_Name = model.Group_Name,
              //  Torneo = await _context.Torneos.FindAsync(model.Torneoid)
            };

        }

        public GroupViewModel ToGroupViewModel(Groups groupEntity)
        {
            return new GroupViewModel
            {
              //  GroupDetails = groupEntity.GroupDetails,
              //  Group_ID = groupEntity.Group_ID,
             //   Matches = groupEntity.Matches,
            //    Group_Name = groupEntity.Group_Name,
                //Torneo = groupEntity.Torneo,
               // Torneoid = groupEntity.Torneo.Torneo_ID
            };

        }
    }



}
