using System;
using System.Collections.Generic;

namespace UGTest.Migrations;

public partial class MusteriFaturaTable
{
    public int Id { get; set; }

    public int? MusteriId { get; set; }

    public DateOnly? FaturaTarihi { get; set; }

    public decimal? FaturaTutari { get; set; }

    public DateOnly? OdemeTarihi { get; set; }
}
