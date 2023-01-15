import React, { useState, useEffect, Fragment } from "react";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Container from "react-bootstrap/Container";
import axios from "axios";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css"

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
    const [Active, setActive] = useState('false');

    const [EditId, setEditId] = useState('00000000-0000-0000-0000-000000000000');
    const [EditTitle, setEditTitle] = useState('');
    const [EditDescription, setEditDescription] = useState('');
    const [EditCreated, setEditCreated] = useState('0001-01-01T00:00:00');
    const [EditModifideDate, setEditModifideDate] = useState('0001-01-01T00:00:00');
    const [EditUrl, setEditUrl] = useState('');
    const [EditActive, setEditActive] = useState('false');

    const [data, setData] = useState([]);
    useEffect(() => {
        getData();
    }, [])

    const getData = () => {
        axios.get('https://localhost:7171/api/movie/getallmovies')
            .then((result) => {
                setData(result.data)
            }).catch((error) => {
                console.log(error)
            })
    }

    const handleEdit = (id) => {
        handleShow();
    }

    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this record?") === true) {
            alert(id);
        }
    }

    const handleUpdate = () => {

    }

    const handleSave = () => {
        const url = "https://localhost:7171/api/movie/addmovie";
        const data = {
            "id": "00000000-0000-0000-0000-000000000000",
            "title": Title,
            "description": Description,
            "created": "0001-01-01T00:00:00",
            "modifideDate": "0001-01-01T00:00:00",
            "url": Url,
            "active": true
        }
        axios.post(url, data)
            .then((result) => {
                getData();
                clear();
                toast.success('Movie was added.');
            })
    }

    const clear = () => {
        setId('00000000-0000-0000-0000-000000000000');
        setTitle('');
        setDescription('');
        setCreated('0001-01-01T00:00:00');
        setModifideDate('0001-01-01T00:00:00');
        setUrl('');
        setActive('false');
        setEditId('00000000-0000-0000-0000-000000000000');
        setEditTitle('');
        setEditDescription('');
        setEditCreated('0001-01-01T00:00:00');
        setEditModifideDate('0001-01-01T00:00:00');
        setEditUrl('');
        setEditActive('false');
    }

    return (
        <Fragment>
            <ToastContainer/>
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
                        <button className="btn btn-primary" onCkick={() => handleSave()}>Submit</button>
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
                                        <td>{index + 1}</td>
                                        <td type="text">{item.title}</td>
                                        <td type="text">{item.description}</td>
                                        <td type="text">{item.url}</td>
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