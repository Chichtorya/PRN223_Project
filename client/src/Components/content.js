import React, { useState } from "react";
import { data } from "../Data/data";
import {
    Form, Input, Modal, Table, Space, Button, Descriptions
} from "antd";
import "./content.css";

export default function DataTable(props) {
    const [open, setOpen] = useState(false);
    const [view, setView] = useState(false);
    const [viewData, setViewData] = useState('');
    const [updateMode, setUpdateMode] = useState(false);

    const [listdt, setdt] = useState(data);
    const [lid, setid] = useState(0);
    const [form] = Form.useForm();

    const columns = [
        {
            title: 'Name',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Email',
            dataIndex: 'email',
            key: 'email',
        },
        {
            title: 'Address',
            dataIndex: 'address',
            key: 'address',
        },
        {
            title: 'Action', dataIndex: '',
            key: 'id',
            render: (text, record) => {
                const isDisabled = record.state === 'Disable';
                return (
                    <Space size="middle">
                        <Button onClick={() => handleView(record)} type="primary" disabled={isDisabled}>View</Button>
                        <Button onClick={() => handleDisable(record.id)} type="primary" danger >Disable</Button>
                        <Button onClick={() => handleUpdate(record)} type="primary" disabled={isDisabled} block>Update</Button>
                    </Space>
                )
            },
        },

    ]
    var lastItem = 0;
    var lastItemId = 0;
    // const [confirmLoading, setConfirmLoading] = useState(false);
    const showModal = () => {
        setOpen(true);
        setUpdateMode(false);
        lastItem = data[data.length - 1];
        lastItemId = lastItem.id + 1;
        setid(lastItemId);
    };

    const handleSubmit = (values) => {
        if (!updateMode) {
            const newData = [...listdt, values];
            form.resetFields();
            setdt(newData);
        }
        else {
            const updatedList = listdt.map(item => {
                if (item.id === values.id) {
                    return { ...item, ...values };
                }
                return item;
            });
            setdt(updatedList);
            setUpdateMode(false);

        }
        setOpen(false);
    }

    const handleCancel = () => {
        setOpen(false);
    };
    const handleView = (rowData) => {
        setView(true);
        setViewData(rowData);
    }
    const handleDisable = (id) => {
        const updatedList = listdt.map(item => {
            if (item.id === id) {
                return { ...item, state: item.state === "able" ? "Disable" : "able" };
            }
            return item;
        });
        setdt(updatedList);
    }
    const handleUpdate = (record) => {
        setUpdateMode(true);
        setOpen(true);
        form.setFieldsValue(record);

    }
    const handleDelete = (id) => {

        const updatedList = listdt.filter(item => item.id !== id);
        if (updatedList) {
            setdt(updatedList);
            setView(false);

        }
    }
    const layout = {
        labelCol: { span: 4 },
        wrapperCol: { span: 20 },
        requiredMark: true
    };
    return (
        <div>
            <Table rowKey="id" dataSource={listdt} columns={columns} rowClassName={record => record.state === "able" ? "able" : "disabled-row"} />
            <Button type="primary" style={{ backgroundColor: "green" }} size="large" onClick={showModal}>New</Button>


            <Modal name="newCus"
                title="New Employee"
                visible={open}
                onOk={() => form.submit()}
                onCancel={() => handleCancel()}
            >
                <Form {...layout} form={form} onFinish={handleSubmit}>

                    <Form.Item label="id" name="id" hidden="hide" initialValue={lid}>
                    </Form.Item>
                    <Form.Item label="state" name="state" hidden="hide" initialValue={"able"}>
                    </Form.Item>
                    <Form.Item label="Name" name="name" rules={[{
                        required: true,
                        message: 'Please input a valid Name!',
                    }]}>
                        <Input />
                    </Form.Item>
                    <Form.Item label="Email" name="email" rules={[
                        {
                            type: 'email',
                            required: true,
                            message: 'Please input a valid email!',
                        },
                    ]}>
                        <Input />
                    </Form.Item>
                    <Form.Item label="Address" name="address">
                        <Input />
                    </Form.Item>
                    <Form.Item label="Phone" name="phone" rules={[
                        {
                            pattern: /^[0-9]{10}$/,
                            message: 'Please input a 10-digit phone number!',
                        },
                    ]}>
                        <Input />
                    </Form.Item>


                </Form>
            </Modal>
            <Modal name="viewCus"
                visible={view}
                onCancel={() => setView(false)}
                okText="Delete"
                okType="danger"
                onOk={() => handleDelete(viewData.id)}

            >

                <Descriptions title="View Employee" bordered>
                    <Descriptions.Item label="Name" span={3}>{viewData.name}</Descriptions.Item>
                    <Descriptions.Item label="Email" span={4}>{viewData.email}</Descriptions.Item>
                    <Descriptions.Item label="Address" span={4}>{viewData.address}</Descriptions.Item>
                    <Descriptions.Item label="Phone" span={4}>{viewData.phone}</Descriptions.Item>
                    <Descriptions.Item label="state" span={2}>{viewData.state === "able" ? "active" : "Disable"}</Descriptions.Item>
                </Descriptions>
            </Modal>
        </div >


    )






}


