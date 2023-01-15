import React, { useState, useEffect, Fragment } from "react";
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';

const MovieCrud = () => {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const [Id, setId] = useState('00000000-0000-0000-0000-000000000000');
    const [Title, setTitle] = useState('');
    const [Description, setDescription] = useState('');
    const [Created, setCreated] = useState('0001-01-01T00:00:00');
    const [ModifideDate, setModifideDate] = useState('0001-01-01T00:00:00');
    const [Url, setUrl] = useState('');
    const [Active, setActive] = useState('flase');

    const [EditId, setEditId] = useState('00000000-0000-0000-0000-000000000000');
    const [EditTitle, setEditTitle] = useState('');
    const [EditDescription, setEditDescription] = useState('');
    const [EditCreated, setEditCreated] = useState('0001-01-01T00:00:00');
    const [EditModifideDate, setEditModifideDate] = useState('0001-01-01T00:00:00');
    const [EditUrl, setEditUrl] = useState('');
    const [EditActive, setEditActive] = useState('flase');

    const movieData = [
        {
            Id: 'e5376999-29b6-49a8-8a39-065a18e85141',
            Title: 'This is the first movie to be added',
            Description: 'about nothing in particular.',
            Created: '2023-01-14 21:45:21.3841971',
            ModifideDate: '2023-01-15 00:41:19.2180590',
            Url: '',
            Active: true
        },
        {
            Id: '4de205d6-173a-49d1-afff-4457b3987ebe',
            Title: 'And the third one',
            Description: 'A steaming pile of crap, but is update still working?',
            Created: '2023-01-15 00:36:57.4898540',
            ModifideDate: '2023-01-15 00:39:15.8771298',
            Url: '',
            Active: true
        },
        {
            Id: 'fdd44cd3-3e46-4a1f-a1e8-eb36d0a3917f',
            Title: 'This is the second movie to be added Part 2',
            Description: 'about nothing in particular except against all odds, it is better than the first one.',
            Created: '2023-01-14 21:46:50.4900749',
            ModifideDate: '2023-01-14 22:12:55.3135867',
            Url: '',
            Active: true
        }
    ]

    const [data, setData] = useState([]);
    useEffect(() => {
        setData(movieData);
    }, [movieData])

    const handleEdit = (id) => {
        //alert(id);
        handleShow();
    }

    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this record?") === true) {
            alert(id);
        }
    }

    const handleUpdate = () => {

    }

    return (
        <Fragment>
            <Container>
                <Row>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter movie name" value={Title} onChange={(e) => setTitle(e.target.value)} />
                    </Col>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter movie description" value={Description} onChange={(e) => setDescription(e.target.value)} />
                    </Col>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter url for movie" value={Url} onChange={(e) => setUrl(e.target.value)} />
                    </Col>
                    <Col>
                        <button className="btn btn-primary">Submit</button>
                    </Col>
                </Row>
            </Container>
            <br />
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Description</th>
                        <th>Url</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        data && data.length > 0 ?
                        data.map((item, index) => {
                            return (
                                <tr key={index}>
                                    <td>{item.Title}</td>
                                    <td>{item.Description}</td>
                                    <td>{item.Url}</td>
                                    <td colSpan={2}>
                                        <button className="btn btn-primary" onClick={() => handleEdit(item.Id)}>Edit</button> &nbsp;
                                        <button className="btn btn-danger" onClick={() => handleDelete(item.Id)}>Delete</button>
                                    </td>
                                </tr>
                            )
                        })
                        :
                        'loading...'
                    }
                </tbody>
            </Table>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Update Movie</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Row>
                        <Col>
                            <input type="text" className="form-control" placeholder="Enter movie name" value={EditTitle} onChange={(e) => setEditTitle(e.target.value)} />
                        </Col>
                        <Col>
                            <input type="text" className="form-control" placeholder="Enter movie description" value={EditDescription} onChange={(e) => setEditDescription(e.target.value)} />
                        </Col>
                        <Col>
                            <input type="text" className="form-control" placeholder="Enter url for movie" value={EditUrl} onChange={(e) => setEditUrl(e.target.value)} />
                        </Col>
                    </Row>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleUpdate}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </Fragment>
    )
}

export default MovieCrud;