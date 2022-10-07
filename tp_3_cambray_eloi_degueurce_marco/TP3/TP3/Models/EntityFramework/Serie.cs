using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP3.Models.EntityFramework;

[Table("t_e_serie_ser")]
public partial class Serie
{
    public Serie()
    {
        NotesSeries = new HashSet<Notation>();
    }

    [Key]
    [Column("ser_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ser_titre")]
    [StringLength(100)]
    public string Titre { get; set; } = null!;
    [Column("ser_resume")]
    public string? Resume { get; set; }

    [Column("ser_nbsaisons")]
    public int? NbSaisons { get; set; }

    [Column("ser_nbepisodes")]
    public int? NbEpsiodes { get; set; }

    [Column("ser_anneecreation")]
    public int? AnneeCreation { get; set; }

    [Column("ser_network")]
    [StringLength(50)]
    public string? Network { get; set; }
    public virtual ICollection<Notation> NotesSeries { get; set; }
}

