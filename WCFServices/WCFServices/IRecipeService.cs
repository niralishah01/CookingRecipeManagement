using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRecipeService" in both code and config file together.
    [ServiceContract]
    public interface IRecipeService
    {
        [OperationContract]
        string AddRecipe(Recipe Recipe);

        [OperationContract]
        Recipe EditRecipe(int Id);

        [OperationContract]
        string UpdateRecipe(Recipe Recipe);

        [OperationContract]
        List<Recipe> GetAllRecipes(int userid);

        [OperationContract]
        Recipe GetRecipe(int id);

        [OperationContract]
        bool AddLike(int id);

        [OperationContract]
        bool AddDislike(int id);

        [OperationContract]
        List<Recipe> GetUserSpecificRecipes(int id);

        [OperationContract]
        bool DeleteRecipe(int id);

        [OperationContract]
        List<Recipe> Search(string searchText);
    }
}
