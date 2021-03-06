﻿using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class DocumentDTO
    {
        //Type of document
        public string Type { get; set; }
        public int Id { get; set; }
        //id and type
        public IDictionary<int, string> CorrelatedDocs { get; set; }
        public FileWrapperDTO File { get; set; } = new FileWrapperDTO();
        public InputOperationsDTO? InputOperation { get; set; }
        public OutputOperationsDTO? OutputOperation { get; set; }
       
    }

    public class FileWrapperDTO
    {
        public byte[] File { get; set; }
        public string Name { get; set; }
    }

    public enum InputOperationsDTO
    {
        Receive, Request,
    }

    public enum OutputOperationsDTO
    {
        Create, Send,
        Update
    }
}