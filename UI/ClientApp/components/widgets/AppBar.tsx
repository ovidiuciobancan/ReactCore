import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';

class Sidebar extends React.Component<{}, {}> {
    public render() {
        return <div>
            
        </div>;
    }
}

//// Wire up the React component to the Redux store
//export default connect(
//    (state: ApplicationState) => state.counter, // Selects which state properties are merged into the component's props
//    CounterStore.actionCreators                 // Selects which action creators are merged into the component's props
//)(Counter) as typeof Counter;