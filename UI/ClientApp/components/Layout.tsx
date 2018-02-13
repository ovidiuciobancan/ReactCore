import * as React from 'react';
import { LayoutView } from 'presentation/Layout/LayoutView';

export class Layout extends React.Component<{}, {}> {
    public render() {
        return (
            <LayoutView {...this.props} />
        );
    }
}
