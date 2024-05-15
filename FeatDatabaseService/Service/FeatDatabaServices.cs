
namespace FeatDatabaseService
{
    public class FeatDatabaseServices
    {

        private PreparedStatements ps = PreparedStatements.CreateInstance();
        

        //method for creating a new feat
        //takes a feat object as parameter
        //calls the InsertFeat method from PreparedStatements
        //returns true if the feat was created successfully, false if not
        public bool CreateFeat(Feat feat)
        {
            try
            {
                return ps.InsertFeat(feat);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //method for updating a feat
        //takes a feat object as parameter
        //calls the UpdateFeat method from PreparedStatements
        //returns true if the feat was updated successfully, false if not
        public bool UpdateFeat(Feat feat)
        {
            try
            {
                return ps.UpdateFeat(feat);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //method for deleting a feat
        //takes a int id as parameter
        //calls the DeleteFeat method from PreparedStatements
        //returns true if the feat was deleted successfully, false if not
        public bool DeleteFeat(int id)
        {
            try
            {
                return ps.DeleteFeat(id);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //method for getting a feat by id
        //takes an int as parameter
        //calls the GetFeatById method from PreparedStatements
        //returns a feat object
        public Feat GetFeatById(int id)
        {
            try
            {
                return ps.GetFeatById(id);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //method for getting all feats
        //calls the GetAllFeats method from PreparedStatements
        //returns a list of feat objects
        public List<Feat> GetAllFeats()
        {
            try
            {
                List<Feat> feats = ps.GetAllFeats();

                if (feats.Count == 0)
                {
                    return null;
                } else
                {
                    return feats;
                }

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}