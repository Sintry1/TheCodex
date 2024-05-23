using System;
using EffectsModel;

namespace EffectsDatabaseService { 

    public class EffectsDatabaseServices
	{

        private PreparedStatements ps = PreparedStatements.CreateInstance();

        //constructor for EffectsServices
        public EffectsDatabaseServices()
		{

		}

		//method calls the InsertEffects method from PreparedStatements
		//takes an Effects object as parameter
		//returns true if the Effects was added successfully, false if not
		public bool AddEffect(Effects effect)
		{
			try
			{
				return ps.InsertEffect(effect);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		//method calls the UpdateEffect method from PreparedStatements
		//takes an Effects object as parameter
		//returns true if the Effects was updated successfully, false if not
		public bool UpdateEffect(Effects effect)
		{
            try
			{
                return ps.UpdateEffect(effect);
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return false;
            }
        }

		//method calls the DeleteEffect method from PreparedStatements
		//takes an int id as parameter
		//returns true if the Effects was deleted successfully, false if not
		public bool DeleteEffect(int id)
		{
            try
			{
                return ps.DeleteEffect(id);
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return false;
            }
        }

		//method calls the GetEffectById method from PreparedStatements
		//takes an int id as parameter
		//returns an Effects object
		public Effects GetEffectById(int id)
		{
            try
			{
                return ps.GetEffectById(id);
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return null;
            }
        }

		//method calls the GetEffects method from PreparedStatements
		//returns a list of Effects objects
		public List<Effects> GetAllEffects()
		{
            try
			{
                return ps.GetAllEffects();
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return null;
            }
        }


	}
}