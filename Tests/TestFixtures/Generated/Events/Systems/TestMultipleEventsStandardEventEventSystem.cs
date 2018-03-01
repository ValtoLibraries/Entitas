//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class TestMultipleEventsStandardEventEventSystem : Entitas.ReactiveSystem<TestEntity> {

    readonly Entitas.IGroup<TestEntity> _listeners;

    public TestMultipleEventsStandardEventEventSystem(Contexts contexts) : base(contexts.test) {
        _listeners = contexts.test.GetGroup(TestMatcher.TestMultipleEventsStandardEventListener);
    }

    protected override Entitas.ICollector<TestEntity> GetTrigger(Entitas.IContext<TestEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(TestMatcher.MultipleEventsStandardEvent)
        );
    }

    protected override bool Filter(TestEntity entity) {
        return entity.hasMultipleEventsStandardEvent;
    }

    protected override void Execute(System.Collections.Generic.List<TestEntity> entities) {
        foreach (var e in entities) {
            var component = e.multipleEventsStandardEvent;
            foreach (var listenerEntity in _listeners) {
                foreach (var listener in listenerEntity.testMultipleEventsStandardEventListener.value) {
                    listener.OnMultipleEventsStandardEvent(e, component.value);
                }
            }
        }
    }
}
