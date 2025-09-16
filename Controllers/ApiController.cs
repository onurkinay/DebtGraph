using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using UGTest.Context;
using UGTest.Controllers;
using UGTest.Models;
using UGTest.Models.Requests; 

namespace UGTest.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : Controller
{
    private readonly MyDbContext db;
    public ApiController(MyDbContext _db)
    {
        db = _db;
    }
   
    [HttpGet]
    [Route("GetAllCustomers")]
    public IActionResult GetAllCustomers() //tüm müşterileri çek
    {
        var customers = db.MusteriTanimTables.ToList();
        if (customers.Count == 0)//müşteriler var mı DB'de
        {
            return NotFound();
        }
        return GenericResponse.JustData(new { user_message = "All customer data successfully", data = customers });
    }

    [HttpGet]
   
    [Route("GetAllCustomerInvoices/{id}")]
    public IActionResult GetAllCustomerInvoices(int id) //istenilen müşterinin faturaları döndür
    {
        var customer = db.MusteriTanimTables.Where(x=> x.Id==id).ToList();
        if (customer.Count == 0)  //müşteri var mı kontrolü
            return GenericResponse.JustData(new { user_message = "Customer not found"});  
        var invoices = db.MusteriFaturaTables.Where(x => x.MusteriId == id).OrderBy(x=>x.FaturaTarihi).ToList();
        
        if (invoices.Count == 0) // müşterinin faturaları var mı
            return GenericResponse.JustData(new { user_message = "Invoices not found for "+ customer[0].Unvan+"/" +id});  
        
        return GenericResponse.JustData(new { user_message = "All customer invoices fetched successfully for "+ customer[0].Unvan +"/"+id, data = invoices});

    }

    [HttpGet]
    [Route("GetCustomerDebt/{id}")]
    public IActionResult GetCustomerDebt(int id) // ilk fatura tarihinden bugüne kadar borçları listele
    {  
        var customer = db.MusteriTanimTables.FirstOrDefault(x => x.Id == id);
        if (customer == null)
            return GenericResponse.JustData(new { user_message = "Customer not found" });

        var invoices = db.MusteriFaturaTables
            .Where(x => x.MusteriId == id)
            .OrderBy(x => x.FaturaTarihi)
            .ToList();

        if (invoices.Count == 0)
            return GenericResponse.JustData(new { user_message = $"Invoices not found for {customer.Unvan}/{id}" });

        // Borç hesabı
        decimal? total = 0;
        DateOnly? firstDate = invoices.First().FaturaTarihi;
        DateOnly? lastDate = DateOnly.FromDateTime(DateTime.Now);

        // Aynı günkü borçlanma ve ödemeleri gruplayıp cache’e al
        var borclarByFatura = invoices
            .GroupBy(x => x.FaturaTarihi)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.FaturaTutari));

        var borclarByOdeme = invoices
            .Where(x => x.OdemeTarihi != null)
            .GroupBy(x => x.OdemeTarihi)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.FaturaTutari));

        var borclar = new List<Debt>();

        for (DateOnly? dt = firstDate; dt <= lastDate; dt = dt.Value.AddDays(1))
        {
            // O gün borç ekle
            if (borclarByFatura.TryGetValue(dt, out var faturaToplam))
                total += faturaToplam;

            // O gün ödeme varsa borçtan düş
            if (borclarByOdeme.TryGetValue(dt, out var odemeToplam))
                total -= odemeToplam;

            borclar.Add(new Debt { tarih = dt, amount = total });
        }

        return GenericResponse.JustData(new
        {
            user_message = $"All customer debts fetched successfully for {customer.Unvan}/{id}",
            debts = borclar
        });
    }
}