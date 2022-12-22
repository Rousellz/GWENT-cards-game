using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWENT_Logic;
[System.Serializable]
public class Card 
{ 
    public Card(int id,string name, int power, string description,string effect,string imageurl,EfectType efectype)
    { 
       //Armor = armor; 
       Name = name;
       Description = description;
       //RecruitmentCost = recruitmentCost;
       Power = power;
       Efect = effect;
       ImageUrl = imageurl;
       Id = id;
       Efecttype= efectype;
    }
    public int Id;
    public string Name;
    public int Power;
    
    public string Description;
    public string Efect;
    public string ImageUrl;
    public EfectType Efecttype;
}


[System.Serializable]
public class CardList
{
    public List<Card> Deck;

}//esta clase es especificamente para leer los json


public enum EfectType { TargetEnemy, TargetAllie, NoTarget, }