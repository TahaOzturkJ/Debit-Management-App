using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.IntRep
{
    public interface IRepository<T> where T:BaseEntity
    {
        //List Commands: Sorgulama içindir.

        List<T> GetAll(); //Bütün Verileri Getirir
        List<T> GetActives(); //Aktif Verileri Getirir
        List<T> GetPassives(); //Pasif Verileri Getirir
        List<T> GetModifieds(); //Düzenlenmiş Verileri Getirir

        //Modify Commands: Veritabanında değişikliğe sebep olacak verileri getirir.

        /// <summary>
        /// Kendini verilen Tipe göre ayarlayarak ekleme yapan metodumuz
        /// </summary>
        /// <param name="item"> Lutfen ilgili Entity tipinde bir argüman veriniz</param>
        void Add(T item); //Ekleme metodu

        /// <summary>
        /// Verinizi pasife ceken metottur..Veriyi yok etmez
        /// </summary>
        /// <param name="item">İlgili entity tipinde argüman giriniz</param>
        void Delete(T item); //Pasife çekme metodu

        void Update(T item); //Veriyi güncelleme metodu

        /// <summary>
        /// Verinizi yok eden metottur. Dikkatli kullanınız
        /// </summary>
        /// <param name="item">İlgili entity tipinde argüman giriniz</param>
        void Destroy(T item); //Veriyi silme metodu

        //Linq Expressions // x => x.ProductName == "Chai"

        List<T> Where(Expression<Func<T, bool>> exp);

        /// <summary>
        /// Veritabanında ilgili ifadeye göre sorguladıgınız yapı var mı yok mu bunu cevaplayan metottur
        /// </summary>
        /// <param name="exp">Expression ifadesi giriniz (T,bool)</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> exp);

        T FirstOrDefault(Expression<Func<T, bool>> exp);

        object Select(Expression<Func<T, object>> exp);

        //Find

        /// <summary>
        /// Primary key'e göre verinizi sorgulayan ve döndüren metottur..
        /// </summary>
        /// <param name="id">Verinizin primary key degerini giriniz</param>
        /// <returns></returns>
        T Find(int id);

    }
}
