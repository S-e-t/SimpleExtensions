# SimpleExtensions

[![NuGet version](https://badge.fury.io/nu/SetApi.SimpleExtensions.svg)](https://badge.fury.io/nu/SetApi.SimpleExtensions) [![Build status](https://ci.appveyor.com/api/projects/status/oj0rppeypm3avael/branch/master?svg=true)](https://ci.appveyor.com/project/S-e-t/simpleextensions/branch/master)

Simple useful extensions for C#.

## Usage

See [Tests](./SimpleExtensions.Test/) for more examples, such as:

 - [build DataTable](./SimpleExtensions.Test/TestDataTableExtensions.cs)
 - work with [IEnumerable](./SimpleExtensions.Test/TestIEnumerableExtensions.cs) and [IDictionary](./SimpleExtensions.Test/TestIDictionaryExtensions.cs)
 - [parse String](./SimpleExtensions.Test/TestStringExtention.cs)

The main purpose of this extension library is to simplify the implementation of the business logic of your application, for example:
```c#
using System;
using SimpleExtensions;
using System.Collections.Generic;
using System.Linq;

public class ViewFactory {

    public IEnumerable<Item> GetItems() =>
         new[] { new Item{ Id = 1, Name = "Item 1", TypeItem = 1, ProviderId = 10, AppsIds = new [] {3,5,7,7 }, Value = "0.15", Start = "2017-12-4" },
                    new Item{ Id = 2, Name = "Item 3", TypeItem = 1, ProviderId = 7, AppsIds = new [] { 3 }, Value = "0.15", Start = "2017-12-4" },
                    new Item{ Id = 3, Name = "Item 4", TypeItem = 2, ProviderId = 10, AppsIds = new int[0]  },
                    new Item{ Id = 4, Name = "Item 5", TypeItem = 2, ProviderId = 8, AppsIds = new [] {5, 5} },
                    new Item{ Id = 5, Name = "Item 5", TypeItem = 2, ProviderId = 10, AppsIds = new [] {3,7 } } };


    public IEnumerable<Apps> GetApps() => new[] {
        new Apps { Id = 3, Assignment = "First"},
        new Apps { Id = 4, Assignment = "Second"},
        new Apps { Id = 5, Assignment = "Last"},
        new Apps { Id = 7, Assignment = "Slightly more than last"}
    };

    public IEnumerable<Provider> GetProviders() =>
         new[] { new Provider{ Id = 10, Name = "Provider 10", Enable = true },
                    new Provider{ Id = 8, Name = "Provider 8", Enable = true },
                    new Provider{ Id = 8, Name = "Provider 8", Enable = true }};

    public enum Event {
        New, Create, More, Empty
    }

    public IEnumerable<ItemHistory> GetItemHistory() => new[] {
        new ItemHistory{ ItemId = 1, Time = "2017-2-23 10:13", Event = "New" },
        new ItemHistory{ ItemId = 1, Time = "2017-2-23 10:13", Event = "New" },
        new ItemHistory{ ItemId = 3, Time = "2017-2-23 10:13", Event = "Create" },
        new ItemHistory{ ItemId = 3, Time = "2017-2-23 10:13", Event = "Create" },
        new ItemHistory{ ItemId = 1, Time = "2017-2-23 10:13", Event = "New" },
        new ItemHistory{ ItemId = 4, Time = "2017-2-23 10:13", Event = "More" },
        new ItemHistory{ ItemId = 16, Time = "2017-2-23 10:13", Event = "Empty" },
    };

    public IEnumerable<ItemType> GetItemTypes() =>
         new[] { new ItemType{ Id = 1, Type = "Food" },
                       new ItemType{ Id = 2, Type = "Thing" },
                       new ItemType{ Id = 3, Type = "Lump" } };


    public ViewModel GetView() {
        var providers = GetProviders().ToDictionaryTry(i => i.Id);
        var itemTypes = GetItemTypes().ToDictionaryTry(i => i.Id);
        var apps = GetApps().ToDictionaryTry(i => i.Id);
        var history = GetItemHistory().GroupByToDictionary(h => h.ItemId, h => new ViewEvent{ Event = h.Event.ToEnum<Event>(), Time = h.Time.ToDateTime() });

        return new ViewModel {
            Items = GetItems().Where(i => providers.ContainsKey(i.ProviderId) && itemTypes.ContainsKey(i.TypeItem))
                        .Select(i => new ViewItem {
                            Id = i.Id,
                            Name = i.Name,
                            Value = i.Value.ToDecimal(),
                            DateStart = i.Start.ToDateTime(),
                            Provider = providers.TryGetValue(i.ProviderId),
                            Type = itemTypes.TryGetValue(i.TypeItem)?.Type ?? "No Type",
                            Apps = i.AppsIds.Where(id => apps.ContainsKey(id)).Select(apps.TryGetValue),
                            History = history.TryGetValue(i.Id) ?? new ViewEvent[0]
                        })
        };
    }
}
....

```

## Installation

The library itself can also be installed in your application using the NuGet package manager.

```
PM> Install-Package SetApi.SimpleExtensions -Version 1.0.1
```
