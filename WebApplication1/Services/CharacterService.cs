using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
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

        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newChar)
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character chara = (_mapper.Map<Character>(newChar));
            _context.Characters.Add(chara);
            await _context.SaveChangesAsync();
            serviceResponse.data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetRandom()
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var dbCharacters = await _context.Characters.ToListAsync();
            int pos = random.Next(dbCharacters.Count());
            serviceResponse.data = _mapper.Map<GetCharacterDTO>(dbCharacters[pos]);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetSingle(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.id == id);
            serviceResponse.data = _mapper.Map<GetCharacterDTO>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdatedCharacterDTO updateChar)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.id == updateChar.id);

                character.name = updateChar.name;
                character.hitPoints = updateChar.hitPoints;
                character.Strength = updateChar.Strength;
                character.Defense = updateChar.Defense;
                character.Intelligence = updateChar.Intelligence;
                character.Class = updateChar.Class;

                await _context.SaveChangesAsync();

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
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.id == id);
                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

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
