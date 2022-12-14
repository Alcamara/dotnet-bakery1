using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DotnetBakery.Models
{
    public enum BreadType
    {
        Sourdough, //0
        Rye,  //1
        French, //2
        HoneyWheat,
    }
    public class Bread 
    {
        public int id {get; set;}
        public string name {get;set;}
        public string description {get; set;}

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BreadType type {get;set;}

        public int count {get;set;}

        [ForeignKey("bakedBy")]
        public int bakedById {get;set;}

        public Baker bakedBy{get; set;}

    }
}
