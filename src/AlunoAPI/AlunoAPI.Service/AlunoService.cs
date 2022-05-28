using AlunoAPI.Data;
using AlunoAPI.Data.Entities;
using FTTBlazor.Client.Common.AlunoAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlunoAPI.Service
{
    public class AlunoService
    {
        #region Private members
        private readonly DatabaseContext _dbContext;
        #endregion

        #region Constructor
        public AlunoService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public AlunoDTO Get(long Id)
        {
            AlunoDTO result = new AlunoDTO();

            try
            {
                result = _dbContext.Alunos.Where(x => x.Id == Id)
                                                .Select(x => Mapping.Mapper.Map<AlunoDTO>(x))
                                                .FirstOrDefault(); ;
            }
            catch (Exception)
            {

            }

            return result;
        }

        public List<AlunoDTO> GetAll(int pagesize, int currentpage)
        {
            List<AlunoDTO> result = new List<AlunoDTO>();

            try
            {
                IQueryable<Aluno> query = _dbContext.Alunos;

                if (currentpage > 0)
                {
                    query = query.Skip(currentpage * pagesize);
                }

                if (pagesize != 0)
                {
                    query = query.Take(pagesize);
                }

                List<Aluno> list = query.ToList();

                result = list.Select(x => Mapping.Mapper.Map<AlunoDTO>(x)).ToList();
            }
            catch (Exception)
            {

            }

            return result;
        }

        public void Add(AlunoDTO dto)
        {
            try
            {
                long newId = _dbContext.Alunos.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();

                dto.Id = (newId + 1).ToString();

                _dbContext.Alunos.Add(Mapping.Mapper.Map<Aluno>(dto));
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Edit(AlunoDTO dto)
        {
            try
            {
                _dbContext.Update(Mapping.Mapper.Map<Aluno>(dto));
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long Id)
        {
            try
            {
                Aluno Aluno = _dbContext.Alunos.Where(x => x.Id == Id).FirstOrDefault(); ;
                _dbContext.Alunos.Remove(Mapping.Mapper.Map<Aluno>(Aluno));
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
