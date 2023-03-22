import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';
import './layout.css';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div className="page-container">
                <Container fluid>
                    <Row>
                        <Col md="15">
                            <Container tag="main">
                                {this.props.children}
                            </Container>
                        </Col>
                    </Row>
                </Container>
            </div>
        );
    }
}