import {shallowMount} from '@vue/test-utils'
import FileUpload from '../../src/components/Timesheet/FileUploader.vue'
import { mount } from '@vue/test-utils'
import sinon from 'sinon'

describe('FileUpload', () => {

	//checks that DOM isn't rendering html since siles are empty
	it('should\'t render since files are empty', () => {
		const propsData = { files: [] }
		const wrapper = shallowMount(FileUpload, {propsData})
		expect(wrapper.element).toMatchSnapshot()
	})

	//checks to see DOM isn't rendering html for bad file.
	it('shouldn\'t render files since bad file', () => {
		var file = [{id: 1, name: 'name', type: 'image', size: '100 * 100 * 100'}]
		const propsData = { files: file }
		const wrapper = shallowMount(FileUpload,
			{propsData})
		expect(wrapper.element).toMatchSnapshot()
})

	//Chekcs if the upload status is not active since no files to upload
	it('upload status should be zero since no files', () => {
		const spy = sinon.spy(FileUpload.methods, 'types')
		const wrapper = mount(FileUpload, {
			propsData: {
				uploadFiles: []
			}
		})
		wrapper.vm.types
		expect(wrapper.vm.uploadStatus).toBe(0)
})

})
