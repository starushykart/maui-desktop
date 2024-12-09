using System.Collections.ObjectModel;
using Hackathon.App.Models;

namespace Hackathon.App.Utilities;

public static class ObservableCollectionExtensions
{
    public static void AddIfNotExist(this ObservableCollection<Document> collection, Document document)
    {
        if (collection.Any(x => x.Id == document.Id))
            return;

        collection.Add(document);
    }
}