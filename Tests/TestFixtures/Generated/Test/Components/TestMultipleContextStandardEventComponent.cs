//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TestEntity {

    public MultipleContextStandardEventComponent multipleContextStandardEvent { get { return (MultipleContextStandardEventComponent)GetComponent(TestComponentsLookup.MultipleContextStandardEvent); } }
    public bool hasMultipleContextStandardEvent { get { return HasComponent(TestComponentsLookup.MultipleContextStandardEvent); } }

    public void AddMultipleContextStandardEvent(string newValue) {
        var index = TestComponentsLookup.MultipleContextStandardEvent;
        var component = CreateComponent<MultipleContextStandardEventComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMultipleContextStandardEvent(string newValue) {
        var index = TestComponentsLookup.MultipleContextStandardEvent;
        var component = CreateComponent<MultipleContextStandardEventComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMultipleContextStandardEvent() {
        RemoveComponent(TestComponentsLookup.MultipleContextStandardEvent);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TestEntity : IMultipleContextStandardEventEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class TestMatcher {

    static Entitas.IMatcher<TestEntity> _matcherMultipleContextStandardEvent;

    public static Entitas.IMatcher<TestEntity> MultipleContextStandardEvent {
        get {
            if (_matcherMultipleContextStandardEvent == null) {
                var matcher = (Entitas.Matcher<TestEntity>)Entitas.Matcher<TestEntity>.AllOf(TestComponentsLookup.MultipleContextStandardEvent);
                matcher.componentNames = TestComponentsLookup.componentNames;
                _matcherMultipleContextStandardEvent = matcher;
            }

            return _matcherMultipleContextStandardEvent;
        }
    }
}
