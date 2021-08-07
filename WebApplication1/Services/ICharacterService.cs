using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Character;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAll();

        Task<ServiceResponse<GetCharacterDTO>> GetRandom();

        Task<ServiceResponse<GetCharacterDTO>> GetSingle(int id);

        Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newChar);

        Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdatedCharacterDTO updateChar);

        Task<ServiceResponse<GetCharacterDTO>> DeleteCharacter(int id);

    }
}
