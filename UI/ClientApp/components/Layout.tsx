import * as React from 'react';
import { bindActionCreators, Dispatch } from 'redux'
import { connect } from 'react-redux'
import * as Mui from 'material-ui'
import MenuIcon from 'material-ui/svg-icons/navigation/menu'
import ArrowLeftIcon from 'material-ui/svg-icons/hardware/keyboard-arrow-left'
import * as colors from 'material-ui/styles/colors'
import { resources } from 'resources/index';
import { paths } from 'app/routes';
import { LayoutView, Auth } from 'presentation/layout';
import { LayoutViewProps } from 'interfaces/ComponentProps';
import { defaultLayoutConfiguration } from 'config/LayoutConfig';
import * as DrawerStore from 'widgets/drawer'
import * as AppBarStore from 'widgets/appBar'

interface ComponentPropsFromDispatch {
    updateDrawer: (config: Mui.DrawerProps) => void,
    updateAppBar: (config: Mui.AppBarProps) => void
}

interface ComponentPropsFromState {
    drawerState: DrawerStore.DrawerState
}

type ComponentProps = ComponentPropsFromState & ComponentPropsFromDispatch & any

class LayoutComponent extends React.Component<ComponentProps, {}> {

    private layoutConfiguration: LayoutViewProps = { ...defaultLayoutConfiguration }

    public constructor(props) {
        super(props);

        this.configureAppBar = this.configureAppBar.bind(this);
        this.configureNavManu = this.configureNavManu.bind(this);

        this.configureAppBar();
        this.configureNavManu();
    }

    public render() {
        return (
            <LayoutView {...this.layoutConfiguration} {...this.props} />
        );
    }

    private configureAppBar() {
        this.layoutConfiguration.appBarProps.config.iconElementRight = <Auth />
        this.layoutConfiguration.appBarProps.config.iconElementLeft = this.icons.arrowLeft
        this.layoutConfiguration.appBarProps.config.onLeftIconButtonClick = () => {
            let openState = this.props.drawerState.muiConfig.open;

            this.props.updateAppBar({
                iconElementLeft: !openState ? this.icons.arrowLeft : this.icons.menu
            })

            this.props.updateDrawer({
                open: !openState
            } as Mui.DrawerProps)
        }
    }

    private configureNavManu() {
        this.layoutConfiguration.navMenuProps.items = [
            {
                content: resources.menu.home,
                routeTo: paths.home,
                glyphiconTag: 'glyphicon-home',
                exact: true
            },
            {
                content: resources.menu.authors,
                routeTo: paths.authors,
            },
            {
                content: resources.menu.counter,
                routeTo: paths.counter,
            },
            {
                content: resources.menu.weather,
                routeTo: paths.fetchData,
            }
        ];
    }

    private iconProps = {
        color: colors.white,
        style: {
            width: 40,
            height: 40,
        }
    }

    private icons = {
        arrowLeft: <ArrowLeftIcon {...this.iconProps} />,
        menu: <MenuIcon {...this.iconProps} />
    }
}

export const Layout = connect(
    (state, ownProps: any) => ({  ...{ drawerState: state.drawer }, ...ownProps }),
    (dispatch: Dispatch<any>) => bindActionCreators({
        updateAppBar: AppBarStore.actionCreators.update,
        updateDrawer: DrawerStore.actionCreators.update
    }, dispatch)
)(LayoutComponent)