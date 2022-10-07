using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TP3.Models.EntityFramework;

[Table("t_j_notation_not")]
public partial class Notation
{
    [Key]
    [Column("ser_id")]
    public int SerieId { get; set; }
    [Key]
    [Column("utl_id")]
    public int UtilisateurId { get; set; }
    [Column("not_note")]
    public int Note { get; set; }
      
    [ForeignKey("SerieId")]
    [InverseProperty("NotesSeries")]
    public virtual Serie SerieNotee { get; set; } = null!;

    [ForeignKey("UtilisateurId")]
    [InverseProperty("NotesUtilisateur")]
    [JsonIgnore]
    public virtual Utilisateur UtilisateurNotant { get; set; } = null!;
}

