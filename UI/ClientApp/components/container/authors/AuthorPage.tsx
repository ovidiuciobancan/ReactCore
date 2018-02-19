import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from 'store/index';
import * as Store from 'store/AuthorsStore';

// At runtime, Redux will merge together...
type AuthorsProps =
    Store.AuthorsState
    & typeof Store.actionCreators
    & RouteComponentProps<{ id: string }>

class AuthorPage extends React.Component<AuthorsProps, {}> {
    componentWillMount() {
        this.props.getAuthor(this.props.match.params.id);
    }

    public render() {
        return <div>
            {this.renderAuthor()}
        </div>;
    }

    private renderAuthor() {
        return <table className='table'>
            <tbody>
                <tr>
                    <td>Name</td>
                    <td>{this.props.author.name}</td>
                </tr>
                <tr>
                    <td>Genre</td>
                    <td>{this.props.author.genre}</td>
                </tr>
                <tr >
                    <td>Age</td>
                    <td>{this.props.author.age}</td>
                </tr>
            </tbody>
        </table>;
    }
}

export default connect(
    (state: ApplicationState) => state.authors,
    Store.actionCreators
)(AuthorPage) as typeof AuthorPage;
