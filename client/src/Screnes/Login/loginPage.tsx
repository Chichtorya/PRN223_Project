import React from "react";
import { Form, Input, Checkbox, Button } from "antd";
// import { useSelector, useDispatch } from 'react-redux';
// import FormItem from 'antd/lib/form/FormItem';
import { FormComponentProps } from 'antd/lib/form';
export interface ILoginProps extends FormComponentProps {
	location: any;
}
export default function loginPage(props: any) {


	const onFinish = (e: any) => {
		// e.preventDefault();\

		// console.log(form);
	}
	return (
		<>
			<Form
				name="normal_login"
				className="login-form"
				onSubmit={
					onFinish
				}
			>
				<Form.Item
					// name="username"


					required={true}


				>
					<Input
						placeholder="Username"
					/>
				</Form.Item>
				<Form.Item
					// name="password"
					required={true}

				>
					<Input

						type="password"
						placeholder="Password"
					/>
				</Form.Item>
				<Form.Item>
					<Form.Item   >
						{/* <Checkbox>Remember me</Checkbox> */}
					</Form.Item>

					<a className="login-form-forgot" href="/">
						Forgot password
					</a>
				</Form.Item>

				<Button type="primary" htmlType="submit" className="login-form-button" >
					Log in
				</Button>
				Or <a href="/">register now!</a>
			</Form>


		</>
	);
}
;
