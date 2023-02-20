using MementoFX.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MementoFX.Persistence.SqlServer.Configuration
{
    public class EventTableMapping
    {
        private Dictionary<Type, string> _mappings;
        private readonly string schemaName;
        public EventTableMapping(string schemaName = "dbo")
        {
            _mappings = new Dictionary<Type, string>();
            this.schemaName = schemaName;
        }

        public EventTableMapping MapEvent(Type eventType, string tableName)
        {
            if (_mappings.ContainsKey(eventType)) throw new InvalidOperationException($"Type {eventType.Name}, is already mapped to a table.");
            _mappings.Add(eventType, tableName);
            return this;
        }
        public EventTableMapping MapEvent<T>(string tableName)
        {
            var eventType = typeof(T);
            return MapEvent(eventType, tableName);
        }
        public EventTableMapping MapEvents(IEnumerable<Type> eventTypes, string tableName)
        {
            foreach (var eventType in eventTypes)
            {
                MapEvent(eventType, tableName);
            }
            return this;
        }
        public EventTableMapping MapEventsOfAggregate(Type aggregateType, string tableName)
        {
            var iApplyEventInterface = typeof(IApplyEvent<>);
            var eventTypes = aggregateType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == iApplyEventInterface)
                .SelectMany(t => t.GetGenericArguments());
            return MapEvents(eventTypes, tableName);
        }
        public EventTableMapping MapEventsOfAggregate<T>(string tableName)
        {
            var aggregateType = typeof(T);
            return MapEventsOfAggregate(aggregateType, tableName);
        }
        public EventTableMapping MapEventsOfAggregate<T>()
        {
            var aggregateType = typeof(T);
            return MapEventsOfAggregate(aggregateType, aggregateType.Name);
        }
        public string GetTableNameOf(Type eventType)
        {
            if (!_mappings.ContainsKey(eventType))
            {
                _mappings.Add(eventType, eventType.Name);
            }

            return $"{schemaName}.{_mappings[eventType]}";
        }
        public string GetTableNameOf<T>()
        {
            return GetTableNameOf(typeof(T));
        }
    }
}
