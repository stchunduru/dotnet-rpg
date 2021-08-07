using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Character;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class CharacterService : ICharacterService
    {

        Random random = new Random();

        private static List<Character> list = new List<Character>
        {
            new Character {name = "Bob", Class = RpgClass.Mage},
            new Character {name = "Fred", Class = RpgClass.Knight, id = 1}
        };

        private static IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newChar)
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character chara = (_mapper.Map<Character>(newChar));
            chara.id = list.Max(c => c.id) + 1;
            list.Add(chara);
            serviceResponse.data = list.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            serviceResponse.data = list.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetRandom()
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            int pos = random.Next(list.Count);
            serviceResponse.data = _mapper.Map<GetCharacterDTO>(list[pos]);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetSingle(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            serviceResponse.data = _mapper.Map<GetCharacterDTO>(list.FirstOrDefault(c => c.id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdatedCharacterDTO updateChar)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            try
            { 
                Character character = list.FirstOrDefault(c => c.id == updateChar.id);

                character.name = updateChar.name;
                character.hitPoints = updateChar.hitPoints;
                character.Strength = updateChar.Strength;
                character.Defense = updateChar.Defense;
                character.Intelligence = updateChar.Intelligence;
                character.Class = updateChar.Class;

                serviceResponse.data = _mapper.Map<GetCharacterDTO>(character);
                return serviceResponse;
            }
            catch (Exception e)
            {
                serviceResponse.success = false;
                serviceResponse.Message = e.Message;
                return serviceResponse;

            }
        }

        public async Task<ServiceResponse<GetCharacterDTO>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Character character = list.FirstOrDefault(c => c.id == id);
                list.Remove(character);

                serviceResponse.data = _mapper.Map<GetCharacterDTO>(character);
                return serviceResponse;
            }
            catch (Exception e)
            {
                serviceResponse.success = false;
                serviceResponse.Message = e.Message;
                return serviceResponse;

            }
        }
    }
}
