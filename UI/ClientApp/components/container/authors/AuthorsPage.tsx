import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from 'store/index';
import * as Store from 'store/AuthorsStore';

// At runtime, Redux will merge together...
type AuthorsProps =
    Store.AuthorsState
    & typeof Store.actionCreators
    & RouteComponentProps<any>

class AuthorsPage extends React.Component<AuthorsProps, {}> {
    componentWillMount() {
        this.props.getAuthors();
    }

    public render() {
        return <div>
            {this.renderAuthors()}
        </div>;
    }

    private renderAuthors() {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Genre</th>
                    <th>Age</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {this.props.authors.map(author =>
                    <tr key={author.id}>
                        <td>{author.name}</td>
                        <td>{author.genre}</td>
                        <td>{author.age}</td>
                        <td> <Link to={`/author/${author.id}`}>View</Link></td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}

export default connect(
    (state: ApplicationState) => state.authors, 
    Store.actionCreators
)(AuthorsPage) as typeof AuthorsPage;
