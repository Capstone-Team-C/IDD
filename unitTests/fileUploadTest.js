import { shallowMount } from '@vue/test-utils'
import FileUpload from '../src/components/FileUpload'

describe('FileUpload', () => {
    const wrapper = shallowMount(FileUpload, {
        data () {
            return {
                message: "Test",
                files: []
            }
        }
    })

})