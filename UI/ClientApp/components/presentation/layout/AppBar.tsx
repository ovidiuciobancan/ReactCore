import * as React from 'react';
import { AppBarWidget } from 'widgets/appBar';
import { AppBarProps } from 'interfaces/ComponentProps'

export const AppBar = (props: AppBarProps) => {
    return (
        <AppBarWidget config={props.config} />
    );
}