namespace ShopAmazing.Web.Data.Entities
{
    public interface IEntity//este e o interface que todas as entidades vai implementar, e a parte generica
    {
        //pelo menos todos tem que ter o Id, para poder implementar com o repositorio generico que vai trabalhar com ete IEntity
        //quando faz o build ve os interface e as classes que os implementam
        int Id { get; set; }

        //podemos fazer mais coisas como por exemplo

        //DateTime CreateDate { get; set; }

        //DateTime UpdateDate { get; set; }

        //DateTime DeleteDate { get; set; }

        //bool IsActive { get; set; }
    }
}
