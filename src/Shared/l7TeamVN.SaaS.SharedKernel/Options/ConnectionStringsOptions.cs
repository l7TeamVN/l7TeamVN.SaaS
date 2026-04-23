namespace l7TeamVN.SaaS.SharedKernel.Options;

public record ConnectionStringsOptions
{
    public string Default { get; set; }

    public string MigrationsAssembly { get; set; }

    public int? CommandTimeout { get; set; }
}
